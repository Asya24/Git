using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Project1
{
    public partial class Form1 : Form
    {
        static int gn = 20;
        string workDir = "";
        string filename;
        int _com = 0;
        string[] track = new string[gn];        //отслеживаемые файлы
        string[] untracked = new string[gn];    //неотслеживаемые 
        string[] command = { "git init", "git clone git://github.com/Asya24/Git.git", "git remote add origin git@github.com:Asya24/Git.git",
        "git push origin master", "git add ", "git rm --cached ", "git commit -m", "git status untracked-files" };

        /*
         * 0 - git init                                             создание репозитория
         * 1 - git clone git://github.com/Asya24/Git.git            получение/связываение с репозиторием на сервере
         * 2 - git remote add origin git@github.com:Asya24/Git.git  для связывания каталога с репозиторием на сервере
         * 3 - git push origin master                               см.выше
         * 4 - git add                                              добавить файл в отслеживаемые
         * 5 - git rm --cached                                      удалить файл из отслеживаемых
         * 6 - git commit -m <комментарий>                          коммит проекта
         * 7 - git status untracked-files                           получить неотслеживаемые файлы
         */

        public Form1()
        {
            InitializeComponent();

            this.openFileDialog1.Multiselect = true;

            listBox1.Visible = false;
            listBox2.Visible = false;
            listBox1.SelectionMode = SelectionMode.MultiSimple;
            listBox2.SelectionMode = SelectionMode.MultiSimple;
            label1.Visible = false;            
            label2.Visible = false;
            button4.Visible = false;    //->
            button5.Visible = false;    //<-

            for (int i = 0; i < gn; i++ )
            {
                track[i] = "";
                untracked[i] = "";
            }
        }

        /// <summary>
        /// Создание процесса, входной параметр команда, которую необходимо выполнить
        /// </summary>
        /// <param name="str"></param>
        private void Action(string acommand)
        {
            if (workDir == "")
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    workDir = open.SelectedPath;
                }
            }

            Process cmd = new Process();
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardInput = true;            
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = workDir;
            cmd.Start();
            cmd.StandardInput.WriteLine(acommand);
            cmd.Close();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                workDir = open.SelectedPath;
            }

            if (!Directory.Exists(workDir + "\\.git"))
            {
                int index = 0;

                Action(command[0]);

                string[] dir = Directory.GetFiles(workDir);

                StreamWriter wr = new StreamWriter("untracked.txt");
                StreamWriter wr1 = new StreamWriter("track.txt");

                foreach (string file in dir)
                {
                    wr.WriteLine(Path.GetFileName(file));
                    untracked[index] = Path.GetFileName(file);
                }
                wr.Close();
                wr1.Close();
                MessageBox.Show("Репозитарий создан");
            }
            else
                MessageBox.Show("В данном каталоге уже существует репозиторий");
        }

        private void получитьССервераToolStripMenuItem_Click(object sender, EventArgs e)    //куда будет копиповаться репозитарий с сервера
        {
            Action(command[1]);
        }

        //git pull обновить

        private void button3_Click(object sender, EventArgs e)  //commit
        {
            _com = 1;
            int index = 0;

            if (workDir == "")
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    workDir = open.SelectedPath;

                    string[] str = File.ReadAllLines("untracked.txt");

                    foreach (string file in str)
                    {
                        listBox2.Items.Add(file);
                    }

                    //string[] dir = Directory.GetFiles(workDir);
                    //foreach (string file in dir)
                    //{
                    //    listBox2.Items.Add(Path.GetFileName(file));
                    //    untracked[index] = Path.GetFileName(file);
                    //    index++;
                    //}
                    label1.Visible = true;
                    listBox1.Visible = true;
                    label2.Visible = true;
                    listBox2.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                }
            }
            else
            {
                //string[] dir = Directory.GetFiles(workDir);
                //foreach (string file in dir)
                //{
                //    listBox2.Items.Add(Path.GetFileName(file));
                //    untracked[index] = Path.GetFileName(file);
                //    index++;
                //}
                label1.Visible = true;
                listBox1.Visible = true;
                label2.Visible = true;
                listBox2.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
            }


        }

        private void button4_Click(object sender, EventArgs e)  // ->
        {
            int count = 0;
            string[] s = new string[gn];

            foreach (string str in listBox1.SelectedItems)
            {                
                s[count] = str;
                listBox2.Items.Add(str);
                for (int i = 0; i < gn; i++ )
                {
                    if (untracked[i] == "")
                    {
                        untracked[i] = str;
                        break;
                    }                        
                }
                count++;
            }            

            for (int i = 0; i < count; i++)
            {
                listBox1.Items.Remove(s[i]);
                for (int j = 0; j < gn; j++ )
                {
                    if (track[j] == s[i])
                    {
                        track[j] = "";
                        break;
                    }                        
                }
            }

            listBox1.SelectedItems.Clear();          
        }

        private void button5_Click(object sender, EventArgs e)  // <-
        {
            int count = 0;
            string[] s = new string[gn];

            foreach (string str in listBox2.SelectedItems)
            {
                s[count] = str;
                listBox1.Items.Add(str);
                for (int i = 0; i < gn; i++)
                {
                    if (track[i] == "")
                    {
                        track[i] = str;
                        break;
                    }
                }
                count++;
            }

            for (int i = 0; i < count; i++)
            {
                listBox2.Items.Remove(s[i]);
                for (int j = 0; j < gn; j++)
                {
                    if (untracked[j] == s[i])
                    {
                        untracked[j] = "";
                        break;
                    }
                }
            }

            listBox2.SelectedItems.Clear();  
        }

        private void button1_Click(object sender, EventArgs e)  //OK
        {
            if (_com == 1)
            {
                string[] st = new string[20];
                int index = 0;

                foreach (string str in listBox1.Items)
                {
                    st[index] = str;
                    index++;
                }

                for (int i = 0; i < index; i++ )
                {
                    Action(command[4] + st[i]);
                }

                foreach (string str in listBox2.Items)
                {
                    Action(command[5] + str);
                }
            }

            MessageBox.Show("Commit выполнен");
            label1.Visible = false;
            label2.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)  //close
        {
            this.Close();
        }
        //git status untracked-files
    }
}
