using System;
using System.ComponentModel;
using Last.fm.Properties;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Settings;
using System.Xml;

namespace Last.fm
{
    struct ToCFile
    {
        public string Artist { get; set; }
        public string Track { get; set; }
        public int PlayCount { set; get; }
    }

    public partial class FrmExport : Form
    {
        int procent;
       // string sessionKey = "empty";
       // string defFile = "settings.alr";
        string ApiKey = "ea13f7fe78f7ef62bbbc5c43dcd6dfcb";
       // string mySecret = "aeaa72cd88242eb7f1437ed8b9937e16";
        string Path { get; set; }

        public int TotalPages(string login, string artist, bool filterByArtist)
        {
            try
            {
                string requestText = String.Empty;
                if (filterByArtist) requestText += "http://ws.audioscrobbler.com/2.0/?method=library.gettracks&api_key=" + ApiKey + "&user=" + login + "&artist=" + artist;
                else requestText += "http://ws.audioscrobbler.com/2.0/?method=library.gettracks&api_key=" + ApiKey + "&user=" + login;
                HttpWebRequest tracksRequest = (HttpWebRequest)WebRequest.Create(requestText);
                tracksRequest.ServicePoint.Expect100Continue = false; // важная строка! из-за её отсутствия сервер ласта рвал соединение

                // Настраиваем параметры запроса
                tracksRequest.UserAgent = "Mozilla/5.0";
                tracksRequest.Method = "GET"; // Указываем метод отправки данных скрипту, в случае с Post обязательно
                tracksRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные
                 tracksRequest.Timeout = 5000; // 

                HttpWebResponse tracksResponse = (HttpWebResponse)tracksRequest.GetResponse();
                string tracksResult = new StreamReader(tracksResponse.GetResponseStream(),
                                               Encoding.UTF8).ReadToEnd();

                if (!tracksResult.Contains("status=\"ok\"")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                    throw new Exception("Трек не добавлен в любимые! Причина - " + tracksResult);
                // MessageBox.Show(tracksResult);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tracksResult);

                // string s = "";
                //string tmp1 = "";
                return Convert.ToInt32(doc.SelectNodes("lfm/tracks")[0].Attributes["totalPages"].Value); // получение атрибута у узла
            }
            catch (Exception e1)
            {
                MessageBox.Show("Ошибка - " + e1.Message);
                return 0;
            }
        }

        public void GetTracks(int mode, string login, string artist, int pages, string path, bool filterByArtist, bool filterByPages)
        {
            procent = 0;
            if (String.IsNullOrEmpty(path)) // проверка на путь к файлу
            {
                path = "default.txt";
            }

            int totalPages = TotalPages(login, artist, filterByArtist);
            if (totalPages == 0) // если нет страниц
                return;
            procent = 5;
            if (filterByPages)
            {
                if (pages > totalPages) // проверка на не привышение страниц
                {
                    DialogResult f = MessageBox.Show("Количество страниц в фильтре больше, чем максимальное! Всего страниц - " + totalPages + ". Продолжить с максимальным количеством страниц?", "Непорядок :)", MessageBoxButtons.YesNo);
                    if (f == DialogResult.Yes)
                        pages = totalPages;
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                pages = totalPages;
            }
            
            //int totalPages = TotalPages(login, null);
           // int pages = 5;
            StreamWriter wr = new StreamWriter(path, false, Encoding.UTF8);
            string parametres = String.Empty;
            string s = "";
            int trackCount = 1;
            List<ToCFile> test = new List<ToCFile>();
            for (int i = 1; i <= pages; i++)
            {
                if (chBSeparate.Checked)
                {
                    s += "____________________________________________\r\n";
                    s += "Page " + i + "\r\n";
                    s += "____________________________________________\r\n";
                }

                string requestText = String.Empty;
                if (filterByArtist) requestText += "http://ws.audioscrobbler.com/2.0/?method=library.gettracks&api_key=" + ApiKey + "&user=" + login + "&page=" + i + "&artist="+artist;
                else requestText += "http://ws.audioscrobbler.com/2.0/?method=library.gettracks&api_key=" + ApiKey + "&user=" + login + "&page=" + i;
               // requestText += "http://ws.audioscrobbler.com/2.0/?method=library.gettracks&api_key=" + ApiKey + "&user=" + login;    
                HttpWebRequest tracksRequest = (HttpWebRequest)WebRequest.Create(requestText);
                tracksRequest.ServicePoint.Expect100Continue = false; // важная строка! из-за её отсутствия сервер ласта рвал соединение

                // Настраиваем параметры запроса
                tracksRequest.UserAgent = "Mozilla/5.0";
                tracksRequest.Method = "GET"; // Указываем метод отправки данных скрипту, в случае с Post обязательно
                tracksRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные
               // tracksRequest.Timeout = 5000; // 

                HttpWebResponse tracksResponse = (HttpWebResponse)tracksRequest.GetResponse();
                string tracksResult = new StreamReader(tracksResponse.GetResponseStream(),
                                               Encoding.UTF8).ReadToEnd();

                if (!tracksResult.Contains("status=\"ok\"")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                    throw new Exception("Произошла ошибка! Причина - " + tracksResult);
                // MessageBox.Show(tracksResult);

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(tracksResult);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                   // continue;
                }

                string tmp1 = "";
                foreach (XmlNode node in doc.SelectNodes("lfm/tracks/track")) // путь к значению по тегам
                {
                    if (mode == 1)
                    {
                        tmp1 = trackCount + ". ";
                        trackCount++;
                        // tmp1 += node.SelectNodes("artist/name")[0].InnerText; // теперь в текущем узле выбираем свои узлы (их по одному, поэтому берём нулевой)
                        tmp1 += node.SelectSingleNode("artist/name").InnerText;
                        tmp1 += " - " + node.SelectSingleNode("name").InnerText; // или так
                        tmp1 += " [" + node.SelectSingleNode("playcount").InnerText + " раз(а)]";
                        tmp1 += Environment.NewLine;
                        s += tmp1;
                    }
                    else if (mode == 2)
                    {
                        test.Add(new ToCFile{ Artist = node.SelectSingleNode("artist/name").InnerText, 
                            PlayCount = Convert.ToInt32(node.SelectSingleNode("playcount").InnerText), 
                            Track = node.SelectSingleNode("name").InnerText}); 
                    }
               }
                if (mode == 2)
                {
                    foreach (ToCFile g in test)
                    {
                        for (int q = 0; q < g.PlayCount; q++)
                        {
                           // if (q % 50 == 0)
                           // {
                                s += g.Artist + " - " + g.Track + "\r\n";
                           // }
                        }
                    }
                    test.Clear(); // чистим список, чтобы по 2 раза не забивать песни

                
                //s += 
                //MessageBox.Show(s);
              
                }
                procent = (i + 1) * (100 / pages);
            }


            wr.Write(s);
            wr.Close();
            MessageBox.Show("Готово!");
            procent = 100;
            if (chbAutoOpen.Checked)
            {
                Process.Start(path);
            }



            //return s;

        }
        public FrmExport(string user)
        {
       
            InitializeComponent();
            cbMode.SelectedIndex = 0;
            tbName.Text = user;
        }

        private void chBFilterByArtist_CheckedChanged(object sender, EventArgs e)
        {
            tbFilterByArtist.Enabled = chBFilterByArtist.Checked;
        }

        private void chBFilterByPages_CheckedChanged(object sender, EventArgs e)
        {
            nUPFilterByPages.Enabled = chBFilterByPages.Checked;
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Текстовые файлы|*txt";
            //opd.InitialDirectory = SystemInformation
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Path = sfd.FileName;
            }
        }

        void BlockElements(bool locking)
        {
            tbFilterByArtist.Enabled = locking;
            tbName.Enabled = locking;
            btnChoosePath.Enabled = locking;
            chBFilterByArtist.Enabled = locking;
            chBFilterByPages.Enabled = locking;
            chBSeparate.Enabled = locking;
            btnGo.Enabled = locking;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BlockElements(false);
                if ((tbName.Text == String.Empty) || ((tbFilterByArtist.Text == String.Empty) && (chBFilterByArtist.Checked)))
                {
                    MessageBox.Show("Не заполнены нужные данные!");
                    return;
                }

                procent = 0;
                tmrMain.Start();
                
                int p1 = cbMode.SelectedIndex + 1;
                string p2 = tbName.Text;
                string p3 = tbFilterByArtist.Text;
                int p4 = (int)nUPFilterByPages.Value;
                bool p6 = chBFilterByArtist.Checked;
                bool p7 = chBFilterByPages.Checked;
                Thread newThread = new Thread(delegate() { GetTracks(p1, p2, p3, p4, Path, p6, p7); });
                newThread.Start();
            }
            catch(Exception e1)
            {
                BlockElements(true);
                procent = 100;
                MessageBox.Show(e1.Message);
                

            }
            
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            if (procent < 100)
            progressBar1.Value = procent;
            if (procent == 100)
            {
                progressBar1.Visible = false;
                tmrMain.Stop();
                BlockElements(true);
                return;
            }
            if (!progressBar1.Visible)
                progressBar1.Visible = true;
        }
    }
}

// если при запросе треков не в
