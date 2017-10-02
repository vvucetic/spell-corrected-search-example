using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autosuggest
{
    public partial class Form1 : Form
    {

        SpellChecker sc = new SpellChecker();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var movies = File.ReadAllLines(@"C:\Projects\POC_DEV\AutoSuggestWithSpellcheck\Autosuggest\movies.txt");
            var sw = new Stopwatch();
            sw.Start();
            sc.Train(File.ReadAllLines(@"C:\Projects\POC_DEV\AutoSuggestWithSpellcheck\Autosuggest\movies.txt"));
            sw.Stop();
            lblSpellChecker.Text = $"Spellcheker initialized in {sw.ElapsedMilliseconds} ms";
            //var result = sc.Suggest("mo").ToList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.TextLength < 3)
                return;

            var sw = new Stopwatch();
            sw.Start();
            var suggestions = sc.Suggest(txtSearch.Text).Take(20);
            sw.Stop();
            lblSuggestionTime.Text = $"Suggestion generated in {sw.ElapsedMilliseconds} ms";
            lbSuggestions.Items.Clear();
            foreach (var suggestion in suggestions)
            {
                lbSuggestions.Items.Add(suggestion);
            }
        }
    }
}
