using System;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Geometry3d;
using System.Globalization;

namespace TeklaAsgn5
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
            Model model=new Model();

            OuterCylinder obj1 = new OuterCylinder();
            obj1.createcylinder();

            InnerBeams obj = new InnerBeams();
            
            double inputAngle=Convert.ToDouble(textBox1.Text);

            if (inputAngle > 360)
            {
                inputAngle = (inputAngle % 360);
            }

            double inputArcLength=Convert.ToDouble(textBox2.Text);

            double k=Math.Tan((Math.PI*inputAngle)/180);
            double r = 4000;
            double x = Math.Sqrt((r * r) / (1 + (k * k)));
            double y = k * x;

            Point startPoint = new Point(x, y, 0);
            Point endPoint = new Point(x, y, 4000);

            obj.createBeams(startPoint, endPoint);

            double phi = (inputArcLength) / (r);
            int count = 1;

            while ((count*phi) <= 2*Math.PI - ((Math.PI*inputAngle)/(180)))
            {
                double k2 = Math.Tan(inputAngle + (count * phi));
                double x2 = Math.Sqrt((r * r) / (1 + (k2 * k2)));
                double y2 = k2 * x2;

                if((count * phi) > 0  && (count * phi) < Math.PI / 2)
                {

                }
                else if ((count * phi) > Math.PI/2 && (count * phi) <= Math.PI)
                {
                    x2 = -x2;
                }
                else if ((count * phi) > Math.PI && (count * phi) <= Math.PI*1.5)             
                {
                    x2 = -x2;
                    y2 = -y2;
                }
                else if((count * phi) > Math.PI*1.5 && (count * phi) <= Math.PI * 2)
                {

                }


                Point secStartPoint = new Point(x2, y2, 0);
                Point secEndPoint = new Point(x2, y2, 4000);
                obj.createBeams(secStartPoint, secEndPoint);
                count++;
            }

            model.CommitChanges();
        }
    }
}
