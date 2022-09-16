using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpOOP__16._09._2022_
{
    public delegate void MyDelegate();
    public abstract class GeoFigure
    {
        protected string _name;
        public GeoFigure()
        {
            _name = "Фигура";
        }

        private bool _Scalable;

        public bool scalable
        {
            get { return _Scalable; }
            set { _Scalable = value; }
        }

        public void printName()
        {
            Console.WriteLine(this._name);
        }

        public abstract float Square();
        public virtual float Square(float _metric)
        {
            return _metric;
        }


    }

    public class Circle : GeoFigure 
    {
        public event MyDelegate _notify {
            add => _notify += InformerConsole;
            remove => _notify += InformerConsole;
        }
        public new string _name = "Окружность"; 
        public string _path = "output.txt";
        public float _radius;
        public override float Square()
        {
           
            float _result = _radius *_radius * 3.14f;
            float _limit = 100f;
            if(_result > _limit)
            {
                _notify += InformerConsole;
            }
            return _result;
        }
        public override float Square(float _metric)
        {
            return _radius * _radius * 3.14f;
        }

        public float Square(float _metric, bool isBase)
        {
            if (isBase)
            {
                return base.Square(_metric);
            }
            else
            {
                return Square(_metric);
            }
        }

        public void printSquare()
        {
            Console.WriteLine("Площадь = {0}", this.Square(this._radius));
        }

        public void printSquare(bool isBase)
        {
            Console.WriteLine("Площадь = {0}", this.Square(this._radius, isBase));
        }

        public void writeSquare()
        {
            string full_path = Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments);
            var sw = new StreamWriter(full_path + "\\" + _path, true);
            sw.WriteLine("Площадь = {0}", this.Square(this._radius));
            sw.Close();
        }
        public void InformerConsole()
        {
            Console.WriteLine("Площадь окружности больше 100");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var myCircle = new Circle();
            MyDelegate _delegate = myCircle.printSquare;
            myCircle._radius = 3;
            myCircle._name = "Круг";
            myCircle.printName();
            //myCircle.printSquare();
            //myCircle.printSquare(true);
            _delegate?.Invoke();
            _delegate += myCircle.writeSquare;
            _delegate?.Invoke();
            _delegate -= myCircle.writeSquare;
            _delegate?.Invoke();
            _delegate -= myCircle.printSquare;
            _delegate?.Invoke();

        }
    }
}
