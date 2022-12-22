using System;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Geometry3d;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        double r,cx = 0, cy = 0;
        Point pointa,pointb,pointc,pointz,pointh1,pointh2,reff,pointh3,pointh4;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void createCylinder(double base1,double topFlat,double height)
        {
            var cylinder = new Beam();
            string pro = "EPD" + base1 + "*" + topFlat + "*10";
            cylinder.Profile.ProfileString = pro;
            cylinder.StartPoint = new Point(0, 0, 0);
            cylinder.EndPoint = new Point(0, 0, height);
            cylinder.Insert();
        }

        double trapeziumRadFinder(double base1, double topflat, double height, double sectionHeight)
        {

            double Area = 0.5 * ((height) * (base1 + topflat));

            double reqRad = (((2 * Area) + (sectionHeight * topflat) - (height * topflat) - (sectionHeight * base1)) / (2 * height));

            MessageBox.Show("Required Radius is:" + reqRad);
            return reqRad;
        }

        void createMiddleBeam(double base1,double topFlat, double height, double sectionHeight)
        {
            double r = trapeziumRadFinder( base1,  topFlat,  height,  sectionHeight);
            var middleBeam = new Beam();
            middleBeam.StartPoint = new Point((base1/2) - r, 0, (height / 2));
            middleBeam.EndPoint = new Point((base1/2) + r, 0, (height / 2));
            middleBeam.Profile.ProfileString = "O300*10";
            middleBeam.Insert();
        }

        void createSlantBeam(double base1,double topFlat,double height)
        {
            var slantBeam = new Beam();
            Point centre = new Point(base1 / 2, 0, height/2);
            double z = (height * ((4 * base1) - height) / ((4 * height) - topFlat + base1));
            double x = (2 * z) - base1 + (height / 2);
            double z0 = (height * ((2 * base1) - height) / ((4 * height) - base1 + topFlat));
            double x0= (2 * z0) - base1 + (height / 2);
            slantBeam.StartPoint = new Point(x0, 0, z0);
            slantBeam.EndPoint = new Point(x, 0, z);
            slantBeam.Profile.ProfileString = "O300*10";
            slantBeam.Insert();
        }

        void ShiftalongHorizontal(Point p10, double dist, int side, double ang)
        {

            double z = p10.Z;

            r = Math.Sqrt(Math.Pow((p10.X - cx), 2) + Math.Pow((p10.Y - cy), 2));
            double k1 = ((p10.Y - cy) / (p10.Y - cx));
            double theta1 = Math.Atan(k1);
            if (p10.X < cx)
            {
                theta1 += Math.PI;
            }



            if (theta1 > 360)
            {
                theta1 %= 360;
            }
            double x0 = cx + (r * Math.Cos(theta1));
            double y0 = cy + (r * Math.Sin(theta1));
            Point point0 = new Point(x0, y0, z);
            if (double.IsNaN(ang))
            {
                theta1 = (ang * Math.PI) / 180;
            }


            switch (side)
            {
                case 1:



                    //r = r + dist;
                    //double x = cx + (r * Math.Cos(theta1));
                    //double y = cy + (r * Math.Sin(theta1));
                    //Point point1 = new Point(x, y, z);
                    //pointz = point1;



                    //--------------------OR----------------//



                    //using triangle 
                     double x = p10.X + (dist + Math.Cos(theta1));
                     double y = p10.Y + (dist + Math.Sin(theta1));
                     Point point1 = new Point(x, y, z);
                     pointz = point1;



                    break;



                case 2:



                    //using pythagoras theorem
                    /*  double phi = theta + Math.Atan(dist / r);
                      r = Math.Sqrt((Math.Pow(dist, 2)) + (Math.Pow(r, 2)));
                      double x5 =cx+(r * Math.Cos(phi));
                      double y5 = cy+(r * Math.Sin(phi));
                      Point point2 = new Point(x5, y5, z);
                      pointz = point2;
                      break;*/



                    //--------------------OR----------------//



                    //using triangle
                    double x5 = p10.X - (dist * Math.Sin(theta1));
                    double y5 = p10.Y + (dist * Math.Cos(theta1));
                    Point point2 = new Point(x5, y5, z);
                    pointz = point2;
                    break;



                case 3:



                    r = r - dist;
                    double x4 = cx + (r * Math.Cos(theta1));
                    double y4 = cy + (r * Math.Sin(theta1));
                    Point point3 = new Point(x4, y4, z);
                    pointz = point3;



                    //--------------------OR----------------//



                    //using triangle
                    /* double x4 = p1.X - (dist + Math.Cos(theta));
                     double y4 = p1.Y - (dist + Math.Sin(theta));
                     Point point3 = new Point(x4, y4, z);
                     pointz = point3;*/
                    break;



                case 4:



                    //using pythagoras theroem
                    double phi1 = theta1 - Math.Atan(dist / r);
                    double len = Math.Sqrt((Math.Pow(dist, 2)) + (Math.Pow(r, 2)));
                    double x6 = cx + (len * Math.Cos(phi1));
                    double y6 = cy + (len * Math.Sin(phi1));
                    Point point4 = new Point(x6, y6, z);
                    pointz = point4;
                    break;



                    //--------------------OR----------------//



                    //using triangle
                    /*  double x6 = p1.X + (dist * Math.Sin(theta));
                      double y6 = p1.Y -(dist * Math.Cos(theta));
                      Point point4 = new Point(x6, y6, z);
                      pointz = point4;
                      break;*/





            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double height;
            if (Height.Text != "")
            {
                 height = Convert.ToDouble(Height.Text);

            }
            else
            {
                height = 3500;
            }
            Model model = new Model();

            reff = new Point(2500, 0, 0);

            if (height > 6500 && height <= 10000)
            {
                reff = new Point(trapeziumRadFinder(5000,3500,3500,height-6500), 0, 0);
            }
            else if( height > 10000)
            {
                reff = new Point(3500 / 2, 0, 0);
            }

            ShiftAlongCircumference(reff, 1, 45);
            
            ShiftAlongCircumference(pointa, 3, 2000);

            pointb = pointc;

            ShiftAlongCircumference(pointa, 3, -2000);

            ShiftalongHorizontal(pointb, 1000, 1,(Math.Atan(3/10)));

            pointh1 = pointz;

            ShiftalongHorizontal(pointc, 1000, 1, Math.Atan(3 / 10));

            pointh2 = pointz;

            ShiftalongHorizontal(pointh1, 300, 2, (Math.Atan(3 / 10)));

            pointh3 = pointz;

            ShiftalongHorizontal(pointh2, -300, 2, (Math.Atan(3 / 10)));

            pointh4 = pointz;

            createBaseCylinder();

            createStorey1Cylinder();

            createTopCylinder();
           
            createFrustum();

            createPlate(height);

            

            model.CommitChanges();
        }

        void createPlate(double height)
        {
            ContourPoint p1 = new ContourPoint(new Point(pointa.X,pointa.Y,pointa.Z+height), new Chamfer(0,0,Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            ContourPoint p2 = new ContourPoint(new Point(pointb.X, pointb.Y, pointb.Z + height), null);
            ContourPoint p5 = new ContourPoint(new Point(pointh2.X, pointh2.Y, pointh2.Z + height), null);
            ContourPoint p4 = new ContourPoint(new Point(pointh1.X, pointh1.Y, pointh1.Z + height), null);
            ContourPoint p3 = new ContourPoint(new Point(pointc.X, pointc.Y, pointc.Z + height), null);

            ContourPlate Plate = new ContourPlate();
            Plate.AddContourPoint(p1);
            Plate.AddContourPoint(p2);
            Plate.AddContourPoint(p4);
            Plate.AddContourPoint(p5);
            Plate.AddContourPoint(p3);
            Plate.Profile.ProfileString = "PL50";
            Plate.Material.MaterialString = "IS2062";
            Plate.Class = "5";
            Plate.Name = "Name";
            Plate.Position.Depth = Position.DepthEnum.MIDDLE;
            Plate.Insert();

            ContourPlate Plate1 = new ContourPlate();
            ContourPoint p6 = new ContourPoint(new Point(pointh3.X, pointh3.Y, pointh3.Z + height), null);
            Plate1.AddContourPoint(p2);
            Plate1.AddContourPoint(p4);
            Plate1.AddContourPoint(p6);
            Plate1.Profile.ProfileString = "PL50";
            Plate1.Material.MaterialString = "IS2062";
            Plate1.Class = "2";
            Plate1.Name = "stiffPlate1";
            Plate1.Position.Depth = Position.DepthEnum.MIDDLE;
            Plate1.Insert();

            ContourPlate Plate2 = new ContourPlate();
            ContourPoint p7 = new ContourPoint(new Point(pointh4.X, pointh4.Y, pointh4.Z + height), null);
            Plate2.AddContourPoint(p3);
            Plate2.AddContourPoint(p5);
            Plate2.AddContourPoint(p7);
            Plate2.Profile.ProfileString = "PL50";
            Plate2.Material.MaterialString = "IS2062";
            Plate2.Class = "2";
            Plate2.Name = "stiffPlate2";
            Plate2.Position.Depth = Position.DepthEnum.MIDDLE;
            Plate2.Insert();
        }

        void createBaseCylinder()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(0, 0, 4000);
            var cylinder = new Beam(p1,p2);
            cylinder.Profile.ProfileString = "O5000*10";
            cylinder.Material.MaterialString = "IS2062";
            cylinder.Class = "1";
            cylinder.Position.Depth=Position.DepthEnum.MIDDLE;
            cylinder.Insert();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double base1, topFlat, height;
            if (Base.Text != "" && TopFlat.Text != "" && Height.Text != "")
            {

                base1 = Convert.ToDouble(Base.Text);
                topFlat = Convert.ToDouble(TopFlat.Text);
                height = Convert.ToDouble(Height.Text);

            }
            else
            {
                base1 = 5000;
                topFlat = 3500;
                height = 3500;
            }
            Model model = new Model();


            createCylinder(base1,topFlat,height);

            createSlantBeam(base1, topFlat, height);

            createMiddleBeam(base1, topFlat, height, height / 2);

            model.CommitChanges();
        }

        void createStorey1Cylinder()
        {
            Point p1 = new Point(0, 0, 4000);
            Point p2 = new Point(0, 0, 6500);
            var cylinder = new Beam(p1, p2);
            cylinder.Profile.ProfileString = "O5000*10";
            cylinder.Material.MaterialString = "IS2062";
            cylinder.Class = "1";
            cylinder.Position.Depth = Position.DepthEnum.MIDDLE;
            cylinder.Insert();
        }

        void createTopCylinder()
        {
            Point p1 = new Point(0, 0, 6500 + (3500)+3500);
            Point p2 = new Point(0, 0, 6500+(3500));
            var cylinder = new Beam(p1, p2);
            cylinder.Profile.ProfileString = "O3500*10";
            cylinder.Material.MaterialString = "IS2062";
            cylinder.Class = "1";
            cylinder.Position.Depth = Position.DepthEnum.MIDDLE;
            cylinder.Insert();
        }

        void createFrustum()
        {
            var cylinder = new Beam();
            cylinder.Profile.ProfileString = "EPD5000*3500*10";
            cylinder.StartPoint = new Point(0, 0, 6500);
            cylinder.EndPoint = new Point(0, 0, 3500+6500);
            cylinder.Position.Depth = Position.DepthEnum.MIDDLE;
            cylinder.Insert();
        }

        public void ShiftAlongCircumference(Point p1, int type, double offset)
        {
            double x1 = p1.X;
            double y1 = p1.Y;
            double z = p1.Z;
            r = Math.Sqrt(Math.Pow((x1 - cx), 2) + Math.Pow((y1 - cy), 2));
            double k = ((p1.Y - cy) / (x1 - cx));
            double theta = Math.Atan(k);
            if (p1.X < cx)
            {
                theta += Math.PI;
            }

            switch (type)
            {
                case 1:
                    offset = (offset * Math.PI) / 180;
                    if (offset > 360)
                    {
                        offset = offset % 360;
                    }
                    double x = cx + (r * Math.Cos(offset + theta));
                    double y = cy + (r * Math.Sin(offset + theta));
                    Point point1 = new Point(x, y, z);
                    pointa = point1;
                    break;



                case 2:
                    double Angle = ((offset * 360) / (2 * Math.PI * r));
                    Angle = (Angle * Math.PI) / 180;
                    double Angle1 = Angle;
                    double x2 = cx + (r * Math.Cos(Angle1 + theta));
                    double y2 = cy + (r * Math.Sin(Angle1 + theta));
                    Point point3 = new Point(x2, y2, z);
                    pointb = point3;
                    break;



                case 3:
                    double theta1 = Math.Asin((offset / 2) / r);
                    theta1 = 2 * theta1;
                    double x3 = cx + (r * Math.Cos(theta1 + theta));
                    double y3 = cy + (r * Math.Sin(theta1 + theta));
                    Point point4 = new Point(x3, y3, z);
                    pointc = point4;
                    break;
            }

            
        }
    }
}
