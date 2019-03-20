using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoadSimulator
{
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
