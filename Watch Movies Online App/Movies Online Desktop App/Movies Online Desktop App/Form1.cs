using Moves_Online_Downloader;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movies_Online_Desktop_App
{
    public partial class Form1 : Form
    {
        private List<Movie> _moviesList;
        private List<Movie> _blackList;
        private IEnumerable<Movie> _moviesListFiltered;
        private BindingList<MovieDataForUser> _moviesBinding;
        private GridType gridModChanged = GridType.AllMovies;// 0 - allMovies 1 - moves to watch, 2 - black list
        enum GridType { AllMovies = 0, RelevanlMovies, NotRelevantMovies };
        private DataManager dataManager;
        private Dictionary<string, string> imagesMap;
        public Form1()
        {
            InitializeComponent();
            _blackList = new List<Movie>();
            dataManager = new DataManager();
            imagesMap = new Dictionary<string,string>();
            dataManager.DefineLocalSpace();
        }
        private object o = new object();
        private void Resorting()
        {
            if (sortListBox.Items.Count > 0)
            {
                IEnumerable<Movie> movies = _moviesListFiltered;
                string sortBy = sortListBox.Items[0].ToString();
                IOrderedEnumerable<Movie> tmpMovies = movies.OrderByDescending(m => m.GetType().GetProperty(sortBy).GetValue(m));
                for (int i = 1; i < sortListBox.Items.Count; i++)
                {
                    int thisI = i;
                    sortBy = Convert.ToString(sortListBox.Items[thisI]);
                    tmpMovies = tmpMovies.ThenByDescending(m => m.GetType().GetProperty(sortBy).GetValue(m));
                }
                _moviesListFiltered = tmpMovies;
            }
            updateBindingList(_moviesListFiltered);
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string valueForSort = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
            if (e.Button.CompareTo(MouseButtons.Left) == 0)
            {
                if (sortListBox.Items.IndexOf(valueForSort) == -1)
                {
                    sortListBox.Items.Add(valueForSort);
                    (new Thread(sort => Resorting())).Start();
                }
            }
            else if (e.Button.CompareTo(MouseButtons.Right) == 0)
            {
                int i = sortListBox.Items.IndexOf(valueForSort);
                if (i != -1)
                {
                    sortListBox.Items.RemoveAt(i);
                    (new Thread(sort => Resorting())).Start();
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            // e.Button
            switch (e.ColumnIndex)
            {
                case 0:
                    if (e.RowIndex > -1)
                    {
                        Movie movie = _moviesListFiltered.ToList().Find(m => m.Title.Equals(dgv[e.ColumnIndex, e.RowIndex].Value));
                        if (e.Button.CompareTo(MouseButtons.Left) == 0)
                        {
                            ProcessStartInfo processStartInfo = new ProcessStartInfo(movie.Url);
                            Process process = new Process();
                            process.StartInfo = processStartInfo;
                            if (process.Start())
                            {
                                // That didn't work
                            }
                        }
                        else if (e.Button.CompareTo(MouseButtons.Right) == 0)
                        {
                            if (gridModChanged.Equals(GridType.RelevanlMovies))
                            {
                                _blackList.Add(movie);
                                dataManager.UpdateBlackList(_blackList);
                                _moviesList.RemoveAll(m => m.Title.ToLower().Equals(movie.Title.ToLower()));
                            }
                            if (gridModChanged.Equals(GridType.NotRelevantMovies))
                            {
                                _blackList.RemoveAll(m => m.Title.ToLower().Equals(movie.Title.ToLower()));
                                _moviesList.Add(movie);
                            }
                            FilterRuner();
                        }
                    }
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    break;

            }
        }



        private void startSearch_Click(object sender, EventArgs e)
        {
            LoaderData();
        }
        private void LoaderData()
        {
            _moviesList = dataManager.GetAllMovies();

            _blackList = dataManager.GetBlackList();

            TittleFiltertextBox.AutoCompleteCustomSource.AddRange(_moviesList.Select(m => m.Title).ToArray());
            if (_blackList != null)
            {
                Parallel.ForEach(_blackList, blackListMovie =>
                {
                    _moviesList.RemoveAll(m=>m.Title.EqualsValue(blackListMovie.Title));
                });
            }
            _moviesListFiltered = _moviesList;
            FilterRuner();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _moviesList = new List<Movie>();
            _moviesListFiltered = new List<Movie>();
            subtittleFilter.AutoCompleteCustomSource.AddRange(Data.subtittles.ToArray());
            categoryFilter.AutoCompleteCustomSource.AddRange(Data.moviesCategory.ToArray());
            yearFilter.AutoCompleteCustomSource.AddRange(Data.YearsLimit.ToArray());
            rateFilter.AutoCompleteCustomSource.AddRange(Data.RateLimit.ToArray());
            lengthFilter.AutoCompleteCustomSource.AddRange(Data.LengthLimit.ToArray());

            LoaderData();
        }

        private void updateBindingList(IEnumerable<Movie> moviesListTemp)
        {
            _moviesBinding = new BindingList<MovieDataForUser>(moviesListTemp.Select(m => m.movieDataForUser).ToArray());
            SetBindingSourceDataSource(_moviesBinding);
            SetCountOfMovies(_moviesBinding.Count);
        }

        public void SetCountOfMovies(long newDataSource)
        {
            if (InvokeRequired)
                Invoke(new Action<long>(SetCountOfMovies), newDataSource);
            else moviesCount.Text = newDataSource.ToString();
        }


        public void SetBindingSourceDataSource(object newDataSource)
        {
            if (InvokeRequired)
                Invoke(new Action<object>(SetBindingSourceDataSource), newDataSource);
            else dataGridView1.DataSource = newDataSource;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            runStatus.Text = "Wait Canceling";
        }


        private IEnumerable<Movie> getTheCurrentList()
        {
            if (gridModChanged.Equals(GridType.AllMovies))
            {
                _moviesList.AddRange(_blackList);
                return _moviesList;
            }
            else if (gridModChanged.Equals(GridType.RelevanlMovies))
            {
                return _moviesList;
            }
            else if (gridModChanged.Equals(GridType.NotRelevantMovies))
            {
                return _blackList;
            }
            return new List<Movie>();
        }

        private void FilterRuner()
        {
            //set the relevant list
            IEnumerable<Movie> temp = getTheCurrentList();

            string tittle = TittleFiltertextBox.Text,
                cat = categoryFilter.Text,
                subs = subtittleFilter.Text,
                year = yearFilter.Text,
                len = lengthFilter.Text,
                rate = rateFilter.Text;


            temp = temp.Where(movie => (movie.Title.ToLower().Contains(tittle.ToLower())));

            temp = temp.Where(movie => movie.Categories.ToLower().Contains(cat.ToLower()));

            temp = temp.Where(movie => movie.Subtittles.ToLower().Contains(subs.ToLower()));

            temp = temp.Where(movie => typeFilterHelper.Contains(movie.Type));

            if (!string.IsNullOrEmpty(year))
            {
                if (Data.YearsLimit.Contains(year))
                {
                    int[] years = HelperFunctions.FeachLimitsFromStr(year, 1900, DateTime.Now.Year);
                    temp = temp.Where(movie => years.First() <= movie.Year && movie.Year <= years.Last());
                    incorrectFilter.Text = "";
                }
                else
                {
                    incorrectFilter.Text = "Incorrect year";
                }
            }
            if (!len.Equals(string.Empty))
            {
                if (Data.LengthLimit.Contains(len))
                {
                    int[] lens = HelperFunctions.FeachLimitsFromStr(len, 0, 600, val => val.ToComparableLength().ToString());
                    temp = temp.Where(movie => lens.First() <= movie.Length && movie.Length <= lens.Last());
                    incorrectFilter.Text = "";
                }
                else
                {
                    incorrectFilter.Text = "Incorrect length";
                }
                //temp = temp.Where(movie => (lengthFilter.Text.Split("-").First().ToComparableLength() <= movie.Length && movie.Length <= lengthFilter.Text.Split("-").Last().ToComparableLength()));
            }

            if (!rate.Equals(string.Empty))
            {
                if (Data.RateLimit.Contains(rate))
                {
                    float[] rates = HelperFunctions.FeachLimitsFromStr<float>(rate, 0, 10);
                    temp = temp.Where(movie => rates.First() <= movie.Rate && movie.Rate <= rates.Last());
                    incorrectFilter.Text = "";
                }
                else
                {
                    incorrectFilter.Text = "Incorrect rate";
                }
            }
            _moviesListFiltered = temp;

            //(new Thread(delegate () {
                Array.ForEach<Movie>(temp.ToArray(), movie =>
                {
                    string outLink;
                    string key = movie.Title + movie.Year + movie.Length;
                    if (!imagesMap.TryGetValue(key, out outLink))
                    {
                        imagesMap.Add(key, movie.ImageLink);
                    }
                });
            //})).Start();

            updateBindingList(_moviesListFiltered.ToList());
            Resorting();
        }
        

        private void TittleFiltertextBox_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private void yearFilter_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private void lengthFilter_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private void categoryFilter_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private void rateFilter_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private void subtittleTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterRuner();
        }

        private List<char> typeFilterHelper = new List<char>();
        private void typeFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                typeFilterHelper.Add(((CheckedListBox)sender).SelectedItem.ToString().ToLower().First());
            }
            else
            {
                typeFilterHelper.Remove(((CheckedListBox)sender).SelectedItem.ToString().ToLower().First());
            }
            FilterRuner();
        }

        private bool GenericValidatingProp(string propText, HashSet<string> setForValidating, string errorMsg)
        {
            bool cancel = (!setForValidating.Contains(propText) && !string.IsNullOrEmpty(propText));
            if (cancel)
            {
                MessageBox.Show(errorMsg);
            }
            return cancel;
        }

        private void movieCategoryTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = GenericValidatingProp(((TextBox)sender).Text, Data.moviesCategory, "Incorrect category");
        }

        private void subtittleTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = GenericValidatingProp(((TextBox)sender).Text, Data.subtittles, "Incorrect subtittles");
        }

        private void yearFilter_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = GenericValidatingProp(((TextBox)sender).Text, Data.YearsLimit, "Incorrect year");
        }

        private void lengthFilter_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = GenericValidatingProp(((TextBox)sender).Text, Data.LengthLimit, "Incorrect length");
        }

        private void rateFilter_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = GenericValidatingProp(((TextBox)sender).Text, Data.RateLimit, "Incorrect rate");
        }

        private void gridChanger_Click(object sender, EventArgs e)
        {
            if (gridModChanged.Equals(GridType.AllMovies))
            {
                gridModChanged = GridType.RelevanlMovies;
                gridChanger.Text = "To not relevant movies";
                gridModText.Text = "Relevant Movies";
            }
            else if (gridModChanged.Equals(GridType.RelevanlMovies))
            {
                gridModChanged = GridType.NotRelevantMovies;
                gridChanger.Text = "To all movies";
                gridModText.Text = "Black List";

            }
            else if (gridModChanged.Equals(GridType.NotRelevantMovies))
            {
                gridModChanged = GridType.AllMovies;
                gridChanger.Text = "To relevant movies";
                gridModText.Text = "All movies";
            }
            FilterRuner();
        }

        public void SetPictureOnpictureBox1(string src)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(SetPictureOnpictureBox1), src);
            else
            {
                pictureBox1.Load(src);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }


        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            string key = ((DataGridView)sender)[0, e.RowIndex].Value.ToString() +
                ((DataGridView)sender)[1, e.RowIndex].Value.ToString() + ((DataGridView)sender)[2, e.RowIndex].Value.ToString().ToComparableLength();

            string link = imagesMap[key];
            if (link != null && !link.EqualsValue("N/A"))
            {
                pictureBox1.LoadAsync(link);
            }
        }
    }
}
