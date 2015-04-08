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
        string dir = "";
        string filename;
        public bool flag = false;
        FileSystemWatcher watcher;

        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            textBox1.Visible = false;
            watcher = new FileSystemWatcher();
            this.openFileDialog1.Multiselect = true;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ds = new FolderBrowserDialog();
            if (ds.ShowDialog() == DialogResult.OK)
            {
                dir = ds.SelectedPath;
                //Process p = Process.Start(@"cmd", @"/k cd " + dir + "\ngit init");

                string par = "git init";//команда, которую вы хотите выполнить
                Process cmd = new Process();//создаем новый объект класса
                cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
                cmd.StartInfo.CreateNoWindow = true;//не создавать окно
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
                cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
                cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
                cmd.Start();//запускаем командную строку
                cmd.StandardInput.WriteLine(par);//вводим команду 
                cmd.Close();

                //if (Directory.Exists(dir + "\\" + ".git"))
                //    MessageBox.Show("Репозитарий создан");
                //else
                //    MessageBox.Show("Что-то пошло не так...");
            }
        }

        private void получитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dir == "")
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    dir = folder.SelectedPath;
                }
            }

            //string par = "git clone git://github.com/Asya24/Git.git";
            string par = "git clone git@github.com:Asya24/Git.git";           
            Process cmd = new Process();//создаем новый объект класса
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
            cmd.StartInfo.CreateNoWindow = true;//не создавать окно
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
            cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
            cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
            cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
            cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
            cmd.Start();//запускаем командную строку
            cmd.StandardInput.WriteLine(par);//вводим команду 
            cmd.Close();

            //string nameDir = "Git";
            //if (Directory.Exists(nameDir))
            //    MessageBox.Show("Репозиторий с сервера получен");
            //else
            //    MessageBox.Show("Репозиторий с сервера не получен :(");
        }

        private void связатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dir == "")
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    dir = folder.SelectedPath;
                }
            }

            //string par = "git push -u origin " + dir;
            string par = "git push -u origin " + dir; 
            Process cmd = new Process();//создаем новый объект класса
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
            //cmd.StartInfo.CreateNoWindow = true;//не создавать окно
            //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
            cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
            cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
            cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
            cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
            cmd.Start();//запускаем командную строку
            cmd.StandardInput.WriteLine(par);//вводим команду 
            cmd.Close();
        }

        private void просмотретьФайлыПроектаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ds = new FolderBrowserDialog();
            ds.ShowDialog();
            dir = ds.SelectedPath;
            //Process p = Process.Start(@"cmd", @"/k cd " + dir + "\ngit status");

            string par = "git status";//команда, которую вы хотите выполнить
            Process cmd = new Process();//создаем новый объект класса
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
            //cmd.StartInfo.CreateNoWindow = true;//не создавать окно
            //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
            cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
            //cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
            cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих            
            cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
            cmd.Start();//запускаем командную строку
            cmd.StandardInput.WriteLine(par);//вводим команду 
            cmd.Close();
        }

        public int _command = 0; //номер команды

        private void создатьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox1.Visible = true;
            _command = 1;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            FolderBrowserDialog delete = new FolderBrowserDialog();

            if (delete.ShowDialog() == DialogResult.OK)
            {
                dir = delete.SelectedPath;

                string par = "git rm " + dir;//команда, которую вы хотите выполнить
                Process cmd = new Process();//создаем новый объект класса
                cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
                //cmd.StartInfo.CreateNoWindow = true;//не создавать окно
                //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
                cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
                //cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих            
                cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
                cmd.Start();//запускаем командную строку
                cmd.StandardInput.WriteLine(par);//вводим команду 
                cmd.Close();


                //Directory.Delete(delete.SelectedPath, true);

                //if (!Directory.Exists(delete.SelectedPath))
                //    MessageBox.Show("Директория удалена");
                //else
                //    MessageBox.Show("Директорию не удалена");
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Добавить файлы?", "", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                DialogResult select = this.folderBrowserDialog1.ShowDialog();
                //DialogResult select = this.openFileDialog1.ShowDialog();
                if (select == DialogResult.OK)
                {
                    foreach (char file in folderBrowserDialog1.SelectedPath)
                    {
                        string par = "git add " + file + "1";//команда, которую вы хотите выполнить
                        Process cmd = new Process();//создаем новый объект класса
                        cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
                        //cmd.StartInfo.CreateNoWindow = true;//не создавать окно
                        //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
                        cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
                        //cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                        cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                        cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
                        cmd.Start();//запускаем командную строку
                        cmd.StandardInput.WriteLine(par);//вводим команду 
                        cmd.Close();
                    }
                }
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //git pull
        }

        private void button1_Click(object sender, EventArgs e) //ОК
        {
            if (_command == 1)
            {
               if (textBox1.Text == "")
                        MessageBox.Show("Введите название директории");
               else
               {
                   FolderBrowserDialog create = new FolderBrowserDialog();
                   if (create.ShowDialog() == DialogResult.OK)
                   {
                       if (Directory.Exists(create.SelectedPath + "\\" + textBox1.Text))
                       {
                           DialogResult answer =  MessageBox.Show("Директория с таким именем существует. Заменить её?", "", MessageBoxButtons.YesNo);
                           if (answer == DialogResult.Yes)
                           {
                               Directory.CreateDirectory(create.SelectedPath + "\\" + textBox1.Text);
                               MessageBox.Show("Директория успешно создана");
                               textBox1.Text = "";
                               textBox1.Visible = false;
                               label1.Visible = false;
                           }
                           else
                               if (answer == DialogResult.No)
                               {
                                   MessageBox.Show("Директория не создана");
                                   textBox1.Text = "";
                                   textBox1.Visible = false;
                                   label1.Visible = false;
                               }
                       }
                       else
                       {
                           Directory.CreateDirectory(create.SelectedPath + "\\" + textBox1.Text);
                           MessageBox.Show("Директория успешно создана");
                           textBox1.Text = "";
                           textBox1.Visible = false;
                           label1.Visible = false;
                       }
                   }                      
               }             

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)  //commit
        {
            if (dir == "")
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    dir = folder.SelectedPath;
                }
            }

            //string par = "git push -u origin " + dir;
            string par = "git commit -am 'Commit'" ;
            Process cmd = new Process();//создаем новый объект класса
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");//задаем имя исполняемого файла
            //cmd.StartInfo.CreateNoWindow = true;//не создавать окно
            //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//спрятать окно
            cmd.StartInfo.RedirectStandardInput = true;// перенаправить вход
            //cmd.StartInfo.RedirectStandardOutput = true;//перенаправить выход
            cmd.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
            cmd.StartInfo.WorkingDirectory = dir;//устанавливаю рабочую директорию
            cmd.Start();//запускаем командную строку
            cmd.StandardInput.WriteLine(par);//вводим команду 
            cmd.Close();
        }

        private void создатьРепозиторийToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
