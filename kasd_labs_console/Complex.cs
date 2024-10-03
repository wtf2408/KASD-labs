using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    struct Complex
    {
        public double Real;
        public double Imaginary;
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        public static Complex operator +(in Complex c1, in Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }
        public static Complex operator -(in Complex c1, in Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }
        public static Complex operator *(in Complex c1, in Complex c2)
        {
            return new Complex(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
                                c1.Real * c2.Imaginary + c1.Imaginary * c2.Real);
        }
        public static Complex operator /(in Complex c1, in Complex c2)
        {
            double module = c2.Module();
            return new Complex((c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / module,
                                (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / module);
        }
        public double Module()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }
        public double Argument()
        {
            return Math.Atan2(Imaginary, Real);
        }
        public override string ToString()
        {
            if (Imaginary >= 0)
            {
                return $"{Real} + {Imaginary}i";
            }
            else
            {
                return $"{Real} - {-Imaginary}i";
            }
        }
    }


}
