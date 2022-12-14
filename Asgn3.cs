using System;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Geometry3d;
using System.Windows.Forms;

namespace TeklaAsgn3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Contour Plate
            //Curved Beam 
            //Radius as input/Length of beam as input
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create curved beam with either the radius or the length of the beam
            double x=Convert.ToDouble(Cbeam.Text);
            Model model = new Model();
            double y = (x / (Math.PI / 2));
            double a = y * Math.Cos(45);
            double b = y * Math.Sin(45);
            ContourPoint point3 = new ContourPoint(new Point(y, 0, 0), null);
            ContourPoint point4 = new ContourPoint(new Point(a, b, 0),  new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            ContourPoint point5 = new ContourPoint(new Point(0, y, 0), null);

            PolyBeam PolyBeam = new PolyBeam();


            PolyBeam.AddContourPoint(point3);
            PolyBeam.AddContourPoint(point4);
            PolyBeam.AddContourPoint(point5);
            
            PolyBeam.Profile.ProfileString = "RHS400*300*8";
            PolyBeam.Finish = "PAINT";
            bool Result = false;
            Result = PolyBeam.Insert();
            model.CommitChanges();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double r=Convert.ToDouble(textBox3.Text);
            Model model = new Model();
            r = r * (1.732);
            ContourPoint p1 = new ContourPoint(new Point(5000,0,0),null);
            ContourPoint p2 = new ContourPoint(new Point(5000+r, 0, 0), new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            ContourPoint p3 = new ContourPoint(new Point(5000+(r/2), (0.866*r), 0), new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            ContourPlate contourPlate = new ContourPlate();
            contourPlate.AddContourPoint(p1);
            contourPlate.AddContourPoint(p2);
            contourPlate.AddContourPoint(p3);
            contourPlate.Profile.ProfileString = "PL10";
            contourPlate.Insert();
            model.CommitChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox3.Text);
            string s = "ROD" + x;
            Model model = new Model();
            Point p1=new Point(20000,20000,0);
            Point p2 = new Point(20000, 20000, 300);
            var beam = new Beam(p1, p2);
            beam.Profile.ProfileString = s;
            beam.Insert();
            model.CommitChanges();
        }
    }
}
