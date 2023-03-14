using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commander
{
    public partial class Form1 : Form
    {
        string exec = @"D:\MyProjects\RollCall Project\Timmy32\Timmy32\Timmy32\bin\Debug\timmy32.exe";

        CommandLineExecutor commander = null;

        public Form1()
        {
            InitializeComponent();
            commander= new CommandLineExecutor(exec);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
          
            var command = "gettime";

            var dics = commander.GetDefaultArguments();
            var datetime=commander.ExecuteCommand(command, dics, new GetDeviceTimeParser());

            MessageBox.Show(datetime.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dics=commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("settime",dics, new DoneParser());

            MessageBox.Show(result.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("enable", dics, new BoolParser());

            MessageBox.Show(result.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("disable", dics, new BoolParser());

            MessageBox.Show(result.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("on", dics, new DoneParser());

            MessageBox.Show(result.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("off", dics, new DoneParser());

            MessageBox.Show(result.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("serialno", dics, new StringParser());

            MessageBox.Show(result.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("productcode", dics, new StringParser());

            MessageBox.Show(result.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var result = commander.ExecuteCommand("clear", dics, new DoneParser());

            MessageBox.Show(result.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
                return;

            var id = textBox1.Text;
            var dics = commander.GetDefaultArguments();
            dics.Add("user-id", id);
            var result = commander.ExecuteCommand("getname", dics, new PersianStringParser());

            MessageBox.Show(result.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var info = commander.ExecuteCommand("info", dics, new DeviceInfoParser());


            var result = "";
            result += nameof(info.Baudrate_ID) + ":" + info.Baudrate_ID + Environment.NewLine;
            result += nameof(info.DateSeperate) + ":" + info.DateSeperate + Environment.NewLine;
            result += nameof(info.DeviceId) + ":" + info.DeviceId + Environment.NewLine;
            result += nameof(info.GlogWarning) + ":" + info.GlogWarning + Environment.NewLine;
            result += nameof(info.Language) + ":" + info.Language + Environment.NewLine;
            result += nameof(info.LockOperate) + ":" + info.LockOperate + Environment.NewLine;
            result += nameof(info.MessageCount) + ":" + info.MessageCount + Environment.NewLine;
            result += nameof(info.PowerOffTime) + ":" + info.PowerOffTime + Environment.NewLine;
            result += nameof(info.ReVerifyTime) + ":" + info.ReVerifyTime + Environment.NewLine;
            result += nameof(info.SlogWarning) + ":" + info.SlogWarning + Environment.NewLine;

            MessageBox.Show(result.ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var info = commander.ExecuteCommand("capacity", dics, new CapacityParser());


            var result = "";
            result += nameof(info.FingerPrintCount) + ":" + info.FingerPrintCount + Environment.NewLine;
            result += nameof(info.GLogCount) + ":" + info.GLogCount + Environment.NewLine;
            result += nameof(info.ManagerCount) + ":" + info.ManagerCount + Environment.NewLine;
            result += nameof(info.PasswordCount) + ":" + info.PasswordCount + Environment.NewLine;
            result += nameof(info.SLogCount) + ":" + info.SLogCount + Environment.NewLine;
            result += nameof(info.UserCount) + ":" + info.UserCount + Environment.NewLine;
            result += nameof(info.WhatCount) + ":" + info.WhatCount + Environment.NewLine;

            MessageBox.Show(result.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        { 
            for(int i=20;i<28;i++)
            {
            
                var dics = commander.GetDefaultArguments();
                dics.Add("user-id", textBox1.Text);
                dics.Add("face-index", i.ToString());

                var face = commander.ExecuteCommand("getface", dics, new StringParser());

                MessageBox.Show("Face " + i + ":" + face);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {

                var dics = commander.GetDefaultArguments();
                dics.Add("user-id", textBox1.Text);
                dics.Add("finger-index", i.ToString());

                var face = commander.ExecuteCommand("getfingerprint", dics, new StringParser());

                MessageBox.Show("Finger " + i + ":" + face);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var logs = commander.ExecuteCommand("logs", dics, new GetLogsParser());

            MessageBox.Show(logs.Count.ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var dics = commander.GetDefaultArguments();
            var users = commander.ExecuteCommand("users", dics, new UserParser());
            MessageBox.Show("Users Count : " + users.Count);

        }
    }
}
