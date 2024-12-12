using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CsharpConMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialize sql connection
            MySqlConnection con = new MySqlConnection();
            //set a connection string
            con.ConnectionString = "server=localhost;user id=root;password=;Database=kccdb;";
            //opening connection
            con.Open();
            //validating connection

            if (con.State == ConnectionState.Open)
            {
                if (button1.Text == "Connect Me")
                {
                    button1.Text = "Disconnect Me";
                    this.Text = "Connected";
                }
                else
                {
                    button1.Text = "Connect Me";
                    this.Text = "Disconnected";
                    con.Close();
                }

            }
        }
    }
}
