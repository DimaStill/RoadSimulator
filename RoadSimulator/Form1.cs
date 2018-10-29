using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoadSimulator
{
    public partial class Form1 : Form
    {
        
        DateTime time = DateTime.Now;
        Random random = new Random();
        List<Car> cars = new List<Car>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        public PictureBox CreatePictureBox()
        {
            PictureBox carPicture = new PictureBox();
            carPicture.Image = RandomCarImage();
            Random randomLane = new Random();
            int Y = randomLane.Next(1, 3) == 1 ? 125 : 200;
            carPicture.Location = new Point(-50, Y);
            carPicture.SizeMode = PictureBoxSizeMode.Zoom;
            carPicture.BackColor = Color.Transparent;
            return carPicture;
        }

        public Bitmap RandomCarImage()
        {
            Random randomImage = new Random();
            switch (randomImage.Next(1, 7))
            {
                case 1:
                    return Properties.Resources.Car1;
                case 2:
                    return Properties.Resources.car2;
                case 3:
                    return Properties.Resources.car3;
                case 4:
                    return Properties.Resources.Car4;
                case 5:
                    return Properties.Resources.Car5;
                case 6:
                    return Properties.Resources.Car6;
                default:
                    return Properties.Resources.Car1;
            }
        }

        public void RoadMove(List<Car> cars)
        {
            do
            {
                
            } while (true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time.AddSeconds(3) <= DateTime.Now)
            {
                int speedCar = random.Next(5, 10);
                PictureBox car = CreatePictureBox();
                Controls.Add(car);
                Car carBot = new Car(car, speedCar);
                cars.Add(carBot);
                time = DateTime.Now;
            }

            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Move();
                Application.DoEvents();
                if (i < cars.Count && cars[i].XBack >= 850)
                {
                    cars.Remove(cars[i--]);
                }
                else
                    foreach (Car otherCar in cars)
                    {
                        if (i < cars.Count && cars[i] != otherCar && cars[i].Y == otherCar.Y && cars[i].XFront > otherCar.XFront &&
                            cars[i].XBack < otherCar.XFront)
                        {
                            if (!cars[i].IsBroken)
                                cars[i].Stop(2, 10);
                            else if (!otherCar.IsBroken)
                                otherCar.Stop(3, 8);
                        }
                        Application.DoEvents();
                    }
            }
        }
    }

    public class Car
    {
        int afterCrashSpeed;

        PictureBox carImage;
        DateTime timeCrash;

        public int Speed { get; set; }

        public int XFront
        {
            get { return carImage.Location.X + carImage.Width; }
        }

        public int XBack
        {
            get { return carImage.Location.X; }
        }

        public int Y
        {
            get { return carImage.Location.Y; }
        }

        public bool IsBroken { get; set; }
        
        public Car(PictureBox carImage, int speed)
        {
            this.carImage = carImage;
            Speed = speed;
        }

        public void Create()
        {

        }

        public void Move()
        {
            if (IsBroken && timeCrash <= DateTime.Now)
            {
                Speed = afterCrashSpeed;
                IsBroken = false;
            }
            if (!IsBroken)
                carImage.Location = new Point(carImage.Location.X + Speed, carImage.Location.Y);

            Application.DoEvents();
        }

        public void Stop(int seconds, int newSpeed)
        {
            IsBroken = true;
            afterCrashSpeed = newSpeed;
            timeCrash = DateTime.Now.AddSeconds(seconds);
        }
    }
}
