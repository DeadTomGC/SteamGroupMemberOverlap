using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections;
//TODO open browser and look at all transmitted information to see profiles

namespace Overlap
{
    public partial class Form1 : Form
    {
        ArrayList ids1 = new ArrayList();
        ArrayList ids2 = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data="";
            
            int page = 1;
            bool diff = true;
            string urlpage = "http://steamcommunity.com/groups/" + textBox1.Text + "/memberslistxml/?xml=1&p=";


            while (diff == true)
            {
                diff = false;
                listBox1.Items.Add(urlpage + page);
                listBox1.Refresh();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlpage + page);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                page++;
                int index = 0;
                while (-1 != (index = data.IndexOf("<steamID64>")))
                {
                    string temp = data.Substring(index + 11, 17);
                    if (ids1.Contains(temp))
                    {

                    }
                    else
                    {
                        diff = true;
                        ids1.Add(temp);
                    }
                    data = data.Substring(index + 28);
                }
            }
            page = 1;
            diff = true;
            urlpage = "http://steamcommunity.com/groups/" + textBox2.Text + "/memberslistxml/?xml=1&p=";
            while (diff == true)
            {
                diff = false;

                listBox1.Items.Add(urlpage + page);
                listBox1.Refresh();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlpage + page);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                page++;
                int index = 0;
                while (-1 != (index = data.IndexOf("<steamID64>")))
                {
                    string temp = data.Substring(index + 11, 17);
                    if (ids2.Contains(temp))
                    {

                    }
                    else
                    {
                        diff = true;
                        ids2.Add(temp);
                    }
                    data = data.Substring(index + 28);
                }
            }

            foreach (string id in ids1)
            {
                foreach (string id2 in ids2)
                {
                    if (id.Equals(id2))
                    {
                        checkedListBox1.Items.Add(id, false);
                    }
                }

            }
            checkedListBox1.Refresh();
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (string id in checkedListBox1.CheckedItems)
            {
                System.Diagnostics.Process.Start("http://steamcommunity.com/profiles/" + id);
            }
        }
    }
}
