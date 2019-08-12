using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        static Socket dinleyiciSoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Thread th;
        private void buttonX1_Click(object sender, EventArgs e)
        {
            th = new Thread(dinleeee);th.Start();
        }
        void dinleeee()
        {
            TcpListener dinle = new TcpListener(IPAddress.Any, int.Parse(textBoxX2.Text));
            dinle.Start();

            richTextBox1.Text = "Bağlantı Bekleniyor..." + "\r";
            dinleyiciSoket = dinle.AcceptSocket();
            richTextBox1.Text = "Bağlantı Sağlandı." + "\r";
            while (true)
            {
                try
                {

                    byte[] gelenData = new byte[256];
                    dinleyiciSoket.Receive(gelenData);

                    string mesaj = (Encoding.UTF8.GetString(gelenData)).Split('\0')[0];
                    if (mesaj == "Process Listing")
                    {
                        Process[] Memory = Process.GetProcesses();

                        for (int i = 0; i < Memory.Length - 1; i++)
                        {
                            dinleyiciSoket.Send(Encoding.UTF8.GetBytes(Memory[i].Id + Memory[i].ProcessName+ Memory[i].StartTime));
                        }
                    }
                    richTextBox1.AppendText("Gelen mesaj: " + mesaj + "\r");

                    if (mesaj.ToLower().StartsWith("exit"))
                    {
                        richTextBox1.AppendText("Bağlantı kapatılıyor." + "\r");
                        dinleyiciSoket.Dispose();
                        break;
                    }
                }
                catch
                {

                    richTextBox1.AppendText("Bağlantı kesildi. Çıkış yapılıyor." + "\r");
                    break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                th.Abort();
                Application.Exit();
            }
            catch
            {
            }
        }
    }
}
