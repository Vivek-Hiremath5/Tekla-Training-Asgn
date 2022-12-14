using System;
using Tekla.Structures.Model.Operations;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.ModelInternal;

namespace TeklaApp1
{
    public partial class Form1 : Form
    {
        public int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x=Convert.ToDouble(textBox1.Text);
                var model = new Model();
                var point = new Point(0, 0, 0);

            var profile = new Profile { ProfileString = "RHS400*300*6" };
            var material = new Material { MaterialString = "Steel_Undefined" };
            var finish = "PAINT";
            var theClass = "3";
            if (checkBox1.Checked.Equals(true)) {
                var point2 = new Point(x, 0, 0); 
                var beam = new Beam(point, point2);
                beam.Profile = profile;
                beam.Material = material;
                beam.Finish = finish;
                beam.Insert();
                model.CommitChanges();

            }
            else if(checkBox2.Checked.Equals(true))
            {
                var point2 = new Point(0, x, 0);
                var beam = new Beam(point, point2);
                beam.Profile = profile;
                beam.Material = material;
                beam.Finish = finish;
                beam.Insert();
                model.CommitChanges();
            }
                
                
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Model model=new Model();
            ContourPoint point3 = new ContourPoint(new Point(5000, 2000, 0), null);
            ContourPoint point4 = new ContourPoint(new Point(2000, 2000, 0), null);
            ContourPoint point5 = new ContourPoint(new Point(0, 4000, 0), null);

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
    }
}
