using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    class Program
    {
        static private ConcurrentBag<Movie> _moviesThreadSafe = new ConcurrentBag<Movie>();
        static private object o = new object();
        static private object updateViewLock = new object();
        static private Dictionary<string, bool> botsIsRuning = new Dictionary<string, bool> { { "SubsMoviesBot", false }, { "PopcorenTimeBot", false } };
        static private object updateMainListLocker = new object();
        static private string nameFile = "DBfileForAllMovies";
        static public bool toText { get; set; }

        static void Main(string[] args)
        {
            //toCancel = false;
            //startSearch.Enabled = false;
            //runStatus.Text = "The search runing";
            //moviesCount.Text = "0";
            //dataGridView1.AutoGenerateColumns = true;

            //if (checkedListBoxSitesChoser.GetItemChecked(0))
            //{
            botsIsRuning["SubsMoviesBot"] = true;
            new SubsMoviesBot().Download(Done, "1935-" + DateTime.Now.Year, "", "");
            //}
            //if (checkedListBoxSitesChoser.GetItemChecked(1))
            //{
            botsIsRuning["PopcorenTimeBot"] = false ;
            //new PopcorenTimeBot().Download(Done, "1900-" + DateTime.Now.Year, "", "");
            //}

            while (isKeepDownload)
            {
                Thread.Sleep(10000);
            }
            Console.WriteLine("the movies are saved");
            Console.WriteLine("press any key to finish");
            Console.ReadLine();
        }
       static bool isKeepDownload = true;
        static public void Done(string v, ConcurrentBag<Movie> tempList)
        {
            lock (updateMainListLocker)
            {
                _moviesThreadSafe.AddRange(tempList);
                botsIsRuning[v] = false;
                if (botsIsRuning.Values.All(bot => bot == false))
                {
                    System.IO.File.WriteAllText(@"c:\" + nameFile, JsonConvert.SerializeObject(_moviesThreadSafe));
                    isKeepDownload = false;
                }
            }
        }

        #region NO NEED 
        //public void SetMoviesThreadSafe(ConcurrentBag<Movie> moviesThreadSafe)
        //{
        //    _moviesThreadSafe = moviesThreadSafe;
        //}


        //delegate void SetStartSearchCallBack(bool value);

        //public void SetStartSearch(bool value)
        //{
        //    if (InvokeRequired)
        //    {
        //        SetStartSearchCallBack d = new SetStartSearchCallBack(SetStartSearch);
        //        Invoke(d, new object[] { value });
        //    }
        //    else
        //    {
        //        startSearch.Enabled = value;
        //    }
        //}


        //private void movieYearTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    string yearTxt = ((TextBox)sender).Text;
        //    e.Cancel = (!Data.GetYearsLimit().Contains(yearTxt));
        //    if (e.Cancel)
        //    {
        //        MessageBox.Show("Incorrect year");
        //    }
        //}


        //private void movieCategoryTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    string categoryText = ((TextBox)sender).Text;
        //    e.Cancel = !(Data.moviesCategory.Contains(categoryText) || categoryText.Length == 0);
        //    if (e.Cancel)
        //    {
        //        MessageBox.Show("Incorrect category");
        //    }
        //}

        //private void subtittleTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    string subtittleText = ((TextBox)sender).Text;
        //    e.Cancel = !(Data.subtittles.Contains(subtittleText) || subtittleText.Length == 0);
        //    if (e.Cancel)
        //    {
        //        MessageBox.Show("Incorrect subtittles");
        //    }
        //}

        //private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    string valueForSort = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
        //    if (e.Button.CompareTo(MouseButtons.Left) == 0)
        //    {
        //        if (sortListBox.Items.IndexOf(valueForSort) == -1)
        //        {
        //            sortListBox.Items.Add(valueForSort);
        //            Resorting();
        //        }
        //    }
        //    else if (e.Button.CompareTo(MouseButtons.Right) == 0)
        //    {
        //        int i = sortListBox.Items.IndexOf(valueForSort);
        //        if (i != -1)
        //        {
        //            sortListBox.Items.RemoveAt(i);
        //            Resorting();
        //        }

        //    }
        //}

        //private void Resorting()
        //{
        //    if (sortListBox.Items.Count > 0)
        //    {
        //        List<Movie> movies = _moviesThreadSafe.ToList();
        //        IOrderedEnumerable<Movie> tmpMovies = movies.OrderBy(m => m.GetType().GetProperty(Convert.ToString(sortListBox.Items[0])).GetValue(m));
        //        for (int i = 1; i < sortListBox.Items.Count; i++)
        //        {
        //            int thisI = i;
        //            tmpMovies = tmpMovies.ThenBy(m => m.GetType().GetProperty(Convert.ToString(sortListBox.Items[thisI])).GetValue(m));
        //        }
        //        updateBindingList(new ConcurrentBag<Movie>(tmpMovies), true);
        //    }
        //}

        //private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dgv = (DataGridView)sender;
        //    switch (e.ColumnIndex)
        //    {
        //        case 0:
        //            if (e.RowIndex > -1)
        //            {

        //                Movie movie = _moviesThreadSafe.ToList().Find(m => m.Title.Equals(dgv[e.ColumnIndex, e.RowIndex].Value));
        //                ProcessStartInfo processStartInfo = new ProcessStartInfo(movie.Url);
        //                Process process = new Process();
        //                process.StartInfo = processStartInfo;
        //                if (process.Start())
        //                {
        //                    // That didn't work
        //                }
        //            }
        //            break;
        //        case 1:
        //        case 2:
        //        case 3:
        //        case 4:
        //            break;

        //    }
        //}

        //public CheckedItemCollection GetCheckedListBoxCheckedItems()
        //{
        //    return checkedListBox.CheckedItems;
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    subtittleTextBox.AutoCompleteCustomSource.AddRange(Data.subtittles);
        //    movieCategoryTextBox.AutoCompleteCustomSource.AddRange(Data.moviesCategory);
        //    movieYearTextBox.AutoCompleteCustomSource.AddRange(Data.GetYearsLimit().ToArray());
        //}

        //private void TittleFiltertextBox_TextChanged(object sender, EventArgs e)
        //{
        //    ConcurrentBag<Movie> moviesThreadSafeTemp = new ConcurrentBag<Movie>(_moviesThreadSafe.Where(movie => movie.Title.ToLower().Contains(TittleFiltertextBox.Text)).ToList());
        //    updateBindingList(moviesThreadSafeTemp, false);
        //}

        //private void StopBtn_Click(object sender, EventArgs e)
        //{
        //    runStatus.Text = "Wait Canceling";
        //    toCancel = true;
        //}

        //public bool GetToCancel()
        //{
        //    return toCancel;
        //}

        //private void loadLocaly_Click(object sender, EventArgs e)
        //{
        //    movieYearTextBox.Text = "1900-" + DateTime.Now.Year;
        //    subtittleTextBox.Text = "";
        //    movieCategoryTextBox.Text = "";
        //    for (int i = 0; i < checkedListBox.Items.Count; i++)
        //        checkedListBox.SetItemChecked(i, true);
        //    toText = true;
        //    Run();
        //}

        //private void startLocaly_Click(object sender, EventArgs e)
        //{
        //    List<Movie> _movieListTemp = System.IO.File.ReadAllText(@"c:\" + nameFile).FromJsonToObject<List<Movie>>();

        //    int[] years = HelperFunctions.FeachYeasFromStr(movieYearTextBox.Text);



        //    _moviesThreadSafe = new ConcurrentBag<Movie>(_movieListTemp.
        //        Where(m =>
        //        (m.Categories.ToLower().Contains(movieCategoryTextBox.Text.ToLower()) &&
        //        (years.First() <= m.Year && m.Year <= years.Last()) &&
        //        (m.Subtittles.Contains(subtittleTextBox.Text.ToLower()))
        //        )).ToList());

        //    updateBindingList(_moviesThreadSafe, true);
        //}
        #endregion NO NEED 

    }
}