using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace getpi
{
  public partial class Form1 : Form
  {
    private int radius;
    private Graphics graph;
    private Random rand;
    private long total;
    private long count;
    public Form1()
    {
      InitializeComponent();
      rand = new Random(DateTime.Now.Millisecond);
      radius = 200;
      graph = this.CreateGraphics();
      this.Show();
      Pen WhitePen = new Pen(Color.White, 2);
      Point p1 = new Point(10, 10);
      Point p2 = new Point(radius * 2 + 12, 10);
      Point p3 = new Point(10, radius * 2 + 12);
      Point p4 = new Point(radius * 2 + 12, radius * 2 + 12);
      graph.DrawLine(WhitePen, p1, p2);
      graph.DrawLine(WhitePen, p1, p3);
      graph.DrawLine(WhitePen, p4, p3);
      graph.DrawLine(WhitePen, p4, p2);
      graph.DrawEllipse(WhitePen, p1.X, p1.Y, radius * 2, radius * 2);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Threading.Thread.Sleep(1000);
      while (true)
      {
        backgroundWorker1.ReportProgress(1);
        System.Threading.Thread.Sleep(1);
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (total % 5000 == 0)
        rand = new Random(DateTime.Now.Millisecond);
      double x = 10 + rand.Next(radius * 2);
      double y = 10 + rand.Next(radius * 2);
      Point p = new Point((int)x, (int)y);
      SolidBrush DrawOut = new SolidBrush(Color.Red);
      SolidBrush DrawIn = new SolidBrush(Color.Green);
      double n = (x - 10 - radius) * (x - 10 - radius)
        + (y - 10 - radius) * (y - 10 - radius);
      double distance = Math.Sqrt(n);

      if (distance < radius)
      {
        graph.FillEllipse(DrawIn, (int)x, (int)y, 3, 3);
        count += 1;
      }
      else
      {
        graph.FillEllipse(DrawOut, (int)x, (int)y, 3, 3);
      }
      total += 1;
      double pi = ((double)count * 4) / (double)total;
      label1.Text = "PI:" + pi.ToString();
      label2.Text = "Bias:" +  Math.Abs((Math.PI - pi)).ToString();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if(!backgroundWorker1.IsBusy)
        backgroundWorker1.RunWorkerAsync();
    }
  }
}
