using System;

namespace ARPGGamepadCore
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Rotates the vector by angle
        /// </summary>
        /// <param name="angle">Angle to rotate in Degrees</param>
        /// <returns></returns>
        public Vector2D Rotate(double angle)
        {
            return new Vector2D((this.X * Math.Cos(angle)) - (this.Y * Math.Sin(angle)),
                (this.X * Math.Sin(angle)) + (this.Y * Math.Cos(angle)));
        }

        public Vector2D Normalize()
        {
            double distance = this.Magnitude;
            return new Vector2D(X / distance, Y / distance);
        }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y));
            }
        }

        public static Vector2D operator *(Vector2D op1, double op2)
        {
            return new Vector2D(op1.X * op2, op1.Y * op2);
        }

        public Vector2D ToScreenPosition(int screenWidth, int screenHeight)
        {
            return new Vector2D(
                X + (screenWidth / 2),
                (Y - (screenHeight / 2)) * -1);
        }

        public Vector2D ToScreenCoordinates(short x, short y, int screenWidth, int screenHeight)
        {
            double screenXPos = ((screenWidth / 2) * x) / (32767);
            double screenYPos = ((screenHeight / 2) * y) / (32767);

            return new Vector2D(screenXPos, screenYPos);
        }

        public Vector2D CirculateCoords()
        {
            double x = X;
            double y = Y;
            var rAngle = Math.Atan2(x, y);
            var angleDirAdjust = 1;
            var angleXValAdjust = 0.0;
            var angleYValAdjust = 0.0;
            //Adjust angle, depending on the quadrant
            if (x >= 0 && y >= 0)
            {
                angleDirAdjust = -1;
                angleXValAdjust = 90.0;
            }
            if (x < 0 && y >= 0)
            {
                angleDirAdjust = -1;
                angleXValAdjust = -90.0;
            }
            if (x < 0 && y < 0)
            {
                angleDirAdjust = -1;
                angleYValAdjust = -180.0;
                angleXValAdjust = -90.0;
            }
            if (x > 0 && y < 0)
            {
                angleDirAdjust = -1;
                angleYValAdjust = -180.0;
                angleXValAdjust = 90.0;
            }

            double xR2, yR2;
            rAngle *= angleDirAdjust;
            if (Math.Abs(y) > Math.Abs(x))
            {
                rAngle = (rAngle.ToDegrees() + angleYValAdjust).ToRadians();
                xR2 = (0 * Math.Cos(rAngle)) - (y * Math.Sin(rAngle));
                yR2 = (0 * Math.Sin(rAngle)) + (y * Math.Cos(rAngle));
            }
            else
            {
                rAngle = (rAngle.ToDegrees() + angleXValAdjust).ToRadians();
                xR2 = (x * Math.Cos(rAngle)) - (0 * Math.Sin(rAngle));
                yR2 = (x * Math.Sin(rAngle)) + (0 * Math.Cos(rAngle));
            }

            return new Vector2D(xR2, yR2); // *sourceVector.Magnitude;
        }
    }
}
