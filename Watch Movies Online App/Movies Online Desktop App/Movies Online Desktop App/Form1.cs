using Moves_Online_Downloader;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movies_Online_Desktop_App
{
    public partial class Form1 : Form
    {
        private string nameFile = "DBfileForAllMovies";
        private IEnumerable<Movie> _moviesList;
        private IEnumerable<Movie> _moviesListFiltered;
        private BindingList<MovieDataForUser> _moviesBinding;
        private FilterSet _filterSet;
        public Form1()
        {
            InitializeComponent();
            _filterSet = new FilterSet();
        }

        private void Resorting()
        {
            if (sortListBox.Items.Count > 0)
            {
                IEnumerable<Movie> movies = _moviesListFiltered;
                IOrderedEnumerable<Movie> tmpMovies = movies.OrderByDescending(m => m.GetType().GetProperty(Convert.ToString(sortListBox.Items[0])).GetValue(m));
                for (int i = 1; i < sortListBox.Items.Count; i++)
                {
                    int thisI = i;
                    tmpMovies = tmpMovies.ThenByDescending(m => m.GetType().GetProperty(Convert.ToString(sortListBox.Items[thisI])).GetValue(m));
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
            switch (e.ColumnIndex)
            {
                case 0:
                    if (e.RowIndex > -1)
                    {
                        Movie movie = _moviesListFiltered.ToList().Find(m => m.Title.Equals(dgv[e.ColumnIndex, e.RowIndex].Value));
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(movie.Url);
                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        if (process.Start())
                        {
                            // That didn't work
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
            _moviesList = System.IO.File.ReadAllText(@"c:\" + nameFile).FromJsonToObject<IEnumerable<Movie>>();
            TittleFiltertextBox.AutoCompleteCustomSource.AddRange(_moviesList.Select(m => m.Title).ToArray());
            _moviesListFiltered = _moviesList;
            FilterRuner();
            Resorting();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _moviesList = new List<Movie>();
            _moviesListFiltered = new List<Movie>();
            subtittleFilter.AutoCompleteCustomSource.AddRange(Data.subtittles);
            categoryFilter.AutoCompleteCustomSource.AddRange(Data.moviesCategory);
            yearFilter.AutoCompleteCustomSource.AddRange(Data.YearsLimit.ToArray());
            rateFilter.AutoCompleteCustomSource.AddRange(Data.RateLimit.ToArray());
            lengthFilter.AutoCompleteCustomSource.AddRange(Data.LengthLimit.ToArray());

            _moviesList = System.IO.File.ReadAllText(@"c:\" + nameFile).FromJsonToObject<IEnumerable<Movie>>();
            TittleFiltertextBox.AutoCompleteCustomSource.AddRange(_moviesList.Select(m => m.Title).ToArray());
            _moviesListFiltered = _moviesList;
            FilterRuner();
            Resorting();
        }

        private void updateBindingList(IEnumerable<Movie> moviesListTemp)
        {
            //IEnumerable <MovieDataForUser> movieDataListTemp = moviesListTemp.Select(m => m.movieDataForUser);
            

            //IList<MovieDataForUser> ll = movieDataListTemp.ToArray();

            _moviesBinding = new BindingList<MovieDataForUser>(moviesListTemp.Select(m => m.movieDataForUser).ToArray());
            SetBindingSourceDataSource(_moviesBinding);
            SetCountOfMovies(_moviesBinding.Count);
        }

        public void SetCountOfMovies(int newDataSource)
        {
            if (InvokeRequired)
                Invoke(new Action<int>(SetCountOfMovies), newDataSource);
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

        private void movieYearTextBox_Validating(object sender, CancelEventArgs e)
        {
            string yearTxt = ((TextBox)sender).Text;
            e.Cancel = (!Data.YearsLimit.Contains(yearTxt));
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect year");
            }
        }


        private void movieCategoryTextBox_Validating(object sender, CancelEventArgs e)
        {
            string categoryText = ((TextBox)sender).Text;
            e.Cancel = !(Data.moviesCategory.Contains(categoryText) || categoryText.Length == 0);
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect category");
            }
        }

        private void subtittleTextBox_Validating(object sender, CancelEventArgs e)
        {
            string subtittleText = ((TextBox)sender).Text;
            e.Cancel = !(Data.subtittles.Contains(subtittleText) || subtittleText.Length == 0);
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect subtittles");
            }
        }


        private void FilterRuner()
        {
            string tittle = TittleFiltertextBox.Text,
                cat = categoryFilter.Text,
                subs = subtittleFilter.Text,
                year = yearFilter.Text,
                len = lengthFilter.Text,
                rate = rateFilter.Text;


            IEnumerable<Movie> temp = _moviesList.Where(movie =>
            (movie.Title.ToLower().Contains(tittle.ToLower())));

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
            updateBindingList(_moviesListFiltered.ToList());
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

        private void yearFilter_Validating(object sender, CancelEventArgs e)
        {
            string yearTxt = ((TextBox)sender).Text;
            e.Cancel = (!Data.YearsLimit.Contains(yearTxt));
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect year");
            }
        }

        private void lengthFilter_Validating(object sender, CancelEventArgs e)
        {
            string lenTxt = ((TextBox)sender).Text;
            e.Cancel = (!Data.LengthLimit.Contains(lenTxt));
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect length");
            }
        }

        private void rateFilter_Validating(object sender, CancelEventArgs e)
        {
            string rateTxt = ((TextBox)sender).Text;
            e.Cancel = (!Data.RateLimit.Contains(rateTxt));
            if (e.Cancel)
            {
                MessageBox.Show("Incorrect rate");
            }
        }


    }
}
