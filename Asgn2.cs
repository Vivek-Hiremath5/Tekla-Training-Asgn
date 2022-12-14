using System;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Geometry3d;
//using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tekla2
{

    public partial class Asgn2 : Form
    {

        public Asgn2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model model = new Model();

            if (model.GetConnectionStatus())
            {
                int count = 0;
                double x = double.Parse(textBox1.Text);
                double y = double.Parse(textBox2.Text);
                Point p1 = new Point(y, y, 0);
                Point p2 = new Point(x+y, 0+y, 0);
                Point p3 = new Point(x+y, x+y, 0);
                Point p4 = new Point(0+y, x+y, 0);
                Point c1 = new Point(0 + y, 0 + y, x);
                Point c2 = new Point(x + y, 0 + y, x);
                Point c3=new Point(x + y, x + y, x);
                Point c4=new Point(0 + y, x + y, x);
                List<Beam> myBeam = new List<Beam>()
                {
                    new Beam(p1,p2),
                    new Beam(p2,p3),
                    new Beam(p3,p4),
                    new Beam(p4,p1),
                    new Beam(p1,c1),
                    new Beam(p2,c2),
                    new Beam(p3,c3),
                    new Beam(p4,c4),
                    new Beam(c1,c2),
                    new Beam(c2,c3),
                    new Beam(c3,c4),
                    new Beam(c4,c1),
                };
                foreach (Beam beam in myBeam)
                {
                    count++;
                    beam.Material.MaterialString = "Steel_Undefined";
                    beam.Profile.ProfileString = "RHS400*300*6";
                    beam.Class = "3";
                    if (count == 2||count==4)
                    {
                        beam.Position.Plane = Position.PlaneEnum.RIGHT;
                        beam.Position.PlaneOffset = 1;
                        beam.Position.RotationOffset = 1;
                        beam.Position.DepthOffset = 1;
                    }
                    if (count == 6 || count == 7)
                    {
                        beam.Position.Depth = Position.DepthEnum.FRONT;
                    }
                    if (count > 8)
                    {
                        beam.Position.Depth = Position.DepthEnum.FRONT;
                        
                        if (count == 10 || count == 12)
                        {
                            beam.Position.Plane = Position.PlaneEnum.RIGHT;
                        }
                    }
                    beam.Insert();
                    model.CommitChanges();
                }
                

            }


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
       