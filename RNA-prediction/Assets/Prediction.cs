using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNA_prediction.Assets
{
    public class Prediction
    {
        private double a { get; set; } = 1.4;
        private double b { get; set; } = 0.3;
      
        private int n { get; set; } = 0;
        public int nbInputUnity { get; set; } = 0;
        public int[] optimalArchitect { get; set; }
        public double[,] w2 { get; set; }
        public double[,] w3 { get; set; }

        public int stepOneStep = 10;
        public int stepManyStep = 3;

        public Matrix<double> HenonSerial;
        public Matrix<double> HenonSerialXY;
        public Matrix<double> e;
        public Matrix<double> predictOneStep;
        public Matrix<double> predictManyStep;

        Random random = new Random();

        public Prediction(double a, double b, int n)
        {
            this.a = a;
            this.b = b;
            this.n = n;
        }
        public Prediction(int n)
        {
            this.n = n;
            this.HenonSerialXY = GenerateHenonSerial(0,0);
            nbInputUnity = this.findNbInputUnity();
            optimalArchitect = this.OptimalNetArchitect(HenonSerial);
            //initInputProt();
            Initialisation(optimalArchitect, 0);
            int nbPeriod = 10;
            double[] period = new double[nbPeriod];
            int indexPeriod = 0;
            while (indexPeriod < nbPeriod)
            {
                period[indexPeriod] = ErrorPeriod();
                indexPeriod++;
            }
        }
        #region TEST: VALEURS MANUELLES
        public void initInputProt()
        {
            e = Matrix<double>.Build.Dense(4, 1);
            e[0, 0] = 0.2;
            e[1, 0] = 0.4;
            e[2, 0] = 0.1;
            e[3, 0] = 0.1;
        }
        #endregion

        public double ErrorPeriod()
        {
            int nbProt = 394;
            int k = optimalArchitect[0];
            int cpt = 1;
            double[] outPutV;
            double[] err = new double[nbProt - optimalArchitect[0]];
            double[] tmp = new double[nbProt - optimalArchitect[0]];
            double result = 0;

            while (k < nbProt)
            {
                //Console.WriteLine("    PROTOTYPE " + cpt);
                outPutV = Learning(optimalArchitect, HenonSerial, k, 0.6, w2, w3);
                err[cpt - 1] = Math.Abs(HenonSerial[k, 0] - outPutV[outPutV.Length - 1]);
                //Console.WriteLine("W2 APRES");
                //Affichage(w2, optimalArchitect[0], optimalArchitect[1]);
                //Console.WriteLine();
                //Console.WriteLine("W3 APRES");
                //Affichage(w3, optimalArchitect[1], optimalArchitect[2]);
                //Console.WriteLine();
                result += err[cpt - 1];
                cpt++;
                k++;
            }   
            return result/err.Length;
        }

        public double FuncX(double x, double y)
        {
            return y + 1 - (this.a * Math.Pow(x,2));
        }
        public double FuncY(double x)
        {
            return this.b * x;
        }

        public double G(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Gprim(double x)
        {
            return Math.Exp(-x) / Math.Pow((1 + Math.Exp(-x)), 2);
        }

        public double I(double x)
        {
            return x;
        }

        public void Initialisation(int[] optimalNet, double initWeight)
        {
            this.w2 = new double[optimalNet[1], optimalNet[0]];
            this.w3 = new double[optimalNet[2], optimalNet[1]];
            //for (int i = 0; i < optimalNet[1]; i++)
            //{
            //    for (int j = 0; j < optimalNet[0]; j++)
            //        w2[i, j] = (initWeight != 0) ? initWeight : GenerateHasardWeight();
            //}
            //for (int i = 0; i < optimalNet[1]; i++)
            //    w3[0, i] = (initWeight != 0) ? initWeight : GenerateHasardWeight();

            w2[0, 0] = 0.1;
            w2[0, 1] = 0.2;


            w2[1, 0] = 0.1;
            w2[1, 1] = 0.2;


            w3[0, 0] = 0.1;
            w3[0, 1] = 0.3;


            //Console.WriteLine("W2 AVANT");
            //Affichage(w2, optimalNet[0], optimalNet[1]);
            //Console.WriteLine("W3 AVANT");
            //Affichage(w3, optimalNet[1], 1);
        }
        public Matrix<double> GenerateHenonSerial(double x, double y)
        {
            HenonSerial = Matrix<double>.Build.Dense(n, 1);
            Matrix<double> result = Matrix<double>.Build.Dense(n+1, 2);
            result[0, 0] = x;
            result[0, 1] = y;
            for (int i = 1; i <= n; i++)
            {
                result[i, 0] = Math.Round(FuncX(result[i-1, 0], result[i-1, 1]), 8);
                result[i, 1] = Math.Round(FuncY(result[i - 1, 0]), 8);
                HenonSerial[i-1, 0] = result[i, 0];
            }
            return result;
        }

        public int[] OptimalNetArchitect(Matrix<double> serial)
        {
            int[] result = new int[3];
            result[0] = this.nbInputUnity;
            result[1] = (this.VerifyNbCachedUnity(serial[nbInputUnity, 0])) + 1;// +1 parce que cela retourne l'indice du tableau
            result[2] = 1;
            return result;
        } 
        public int findNbInputUnity()
        {
            int result = 0;
            double[] errorApprox;
            double[] arrayEigenValues = new double[n];
            int[] netOptimal = { 0, 0, 1 };
            Matrix<double> teta = HenonSerial.Multiply(HenonSerial.Transpose());
            Evd<double> eigen = teta.Evd();
            Matrix<double> eigenvector = eigen.EigenVectors;
            MathNet.Numerics.LinearAlgebra.Vector<double> eigenvalues = eigen.EigenValues.Real();
            for (int i = 0; i < eigenvalues.Count; i++)
                arrayEigenValues[i] = eigenvalues[i];
            Array.Sort<double>(arrayEigenValues, new Comparison<double>((a, b) => -a.CompareTo(b)));
            //Array.Sort<double>(arrayEigenValues, delegate (double m, double n) { return (int)n - (int)m; });
            errorApprox = ErrorApproximation(arrayEigenValues);
            for (int i = 0; i < errorApprox.Length; i++)
            {
                if (errorApprox[i] == errorApprox[i + 1])
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public double[] ErrorApproximation(double[] arr)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
                result[i] = Math.Round(Math.Sqrt(Math.Abs(arr[i])), 8);       
            return result;
        }
        public double GenerateHasardWeight()
        { 
            return Math.Round(random.NextDouble() * (0.4 - 0.1) + 0.1, 2);//minValue = 0.1, maxValue = 0.4
        }

        public int VerifyNbCachedUnity(double desiredOutput)
        {
            int uniteCachedLimit = this.nbInputUnity;
            int nbCachedUnity = uniteCachedLimit;
            LinkedList<double> tab = new LinkedList<double>();
            LinkedList<double> tab1 = new LinkedList<double>();
            double[] tabErrorUnity = new double[uniteCachedLimit];
            double[] tmp = new double[uniteCachedLimit];
            double err = 0;
            int result = 0;
            int k = 1;
            while(k < 3)
            {
                err = TestCachedUnity(k, desiredOutput, 0.1);
                tab.AddLast(err);
                k++;
            }
            for (int i = 0; i < tab.Count; i++)
                tab1.AddLast(tab.ElementAt(i));
            double cool = tab1.Min();
            result = Array.FindIndex(tab.ToArray(), x => x == cool);
            return result;
        }

        public double TestCachedUnity(int nbCachedUnity, double desiredOutput, double initWeight)
        {
            double result = 0;
            double[,] w2 = new double[nbCachedUnity, this.nbInputUnity];
            double[,] w3 = new double[1, nbCachedUnity];
            double[] v = new double[this.nbInputUnity + nbCachedUnity + 1];
            int k = 0;
            int cpt = 0;
            double som = 0;
            while (k < 500)
            {
                for (int i = 0; i < nbCachedUnity; i++)
                {
                    for (int j = 0; j < this.nbInputUnity; j++)
                        w2[i, j] = (initWeight != 0)? initWeight : GenerateHasardWeight();
                }
                for (int i = 0; i < nbCachedUnity; i++)
                    w3[0, i] = (initWeight != 0)? initWeight : GenerateHasardWeight();
                k= (k==0)?0:(k - (nbInputUnity - 1));
                for (int i = 0; i < nbInputUnity; i++)
                {
                    v[i] = HenonSerial[k, 0];
                    k++;  
                }
                   
                //v[i] = HenonSerial[i, 0];

                for (int i = 0; i < nbCachedUnity; i++)
                    v[nbInputUnity + i] = ActivationRule(w2, this.nbInputUnity, v, i, false,false);
                int index = nbInputUnity + nbCachedUnity;
                v[index] = ActivationRule(w3, nbCachedUnity, v, 0, true,false);
                result = desiredOutput - v[index];
                som += result;
                cpt++;
            }
            cpt--;
            return Math.Round(som / cpt, 8);
            //return result;
        }

        public double ActivationRule(double[,] w, int nbInput, double[] v, int unity, bool isCachedLayer, bool isPredict)
        {
            double result;
            double h = 0;
            if (!isCachedLayer)
            {
                for (int i = 0; i < nbInput; i++)
                    h = h + w[unity, i] * v[i];
            }
            else
            {
                for (int i = 0; i < nbInput; i++)
                    h = h + w[unity, i] * v[this.nbInputUnity + i];
            }
            result = isPredict? I(h) : G(h);
            return Math.Round(result,8);
        }

        public void Affichage(double[,] w, int input, int cached)
        {
            for (int i = 0; i < cached; i++)
            {
                for (int j = 0; j < input; j++)
                {
                    Console.WriteLine("w[" + i + "," + j + "]: " + w[i, j]);
                }
            }
        }

        public double[] Learning(int[] netArchitect, Matrix<double> serial, int indexOutput, double pas, double[,] w2, double[,] w3)
        {
            int nb = netArchitect[1] + netArchitect[2];
            double[,] result = new double[nb, netArchitect[0]];
            double[] v;
            double[] delta = new double[nb];
            double deltaW = 0;
            double h = 0;
            v = CalculAllActivationRule(netArchitect, indexOutput, w2, w3, serial,true);
            int index = netArchitect[0] + netArchitect[1];
            for (int i = 0; i < netArchitect[1]; i++)
                h = h + w3[0, i] * v[(netArchitect[0] + i)];
            delta[nb - 1] = Gprim(h) * (serial[indexOutput, 0] - v[index]);   
            for (int i = 0; i < netArchitect[1]; i++)
            {
                h = 0;
                for (int j = 0; j < netArchitect[0]; j++)
                    h = h + w2[i, j] * v[j];
                delta[i] = Gprim(h) * w3[0,i]*delta[nb - 1];
            }
            for (int i = 0; i < netArchitect[1]; i++)
            {
                for (int j = 0; j < netArchitect[0]; j++)
                {
                    deltaW = pas * delta[i] * v[j];
                    w2[i, j] = Math.Round((w2[i, j] + deltaW), 8);
                }
            }
            for (int j = 0; j < netArchitect[1]; j++)
            {
                deltaW = pas * delta[nb-1] * v[netArchitect[0] + j];
                w3[0, j] = Math.Round((w3[0, j] + deltaW), 8);
            }
            return v;
        }
        
        public double[] oneStepPrediction(int[] netArchitect, int indexOutput, double[,] w2, double[,] w3, Matrix<double> serial)
        {
            double[] v;
            double[] result = new double[2];
            v = CalculAllActivationRule(netArchitect, indexOutput, w2, w3, serial,true);
            result[0] = serial[indexOutput, 0];
            result[1] = v[v.Length - 1];
            return result;
        }

        public Matrix<double> oneStepProcess(int[] netArchitect, int initPrototype, int nbValPredict, Matrix<double> serial)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(nbValPredict, 2);
            double[] tmp = new double[2];
            int k = (initPrototype + netArchitect[0]) - 1;
            int i = 0;
            while (k < ((initPrototype + netArchitect[0]) - 1  + nbValPredict))
            {
                tmp = oneStepPrediction(netArchitect, k,w2,w3,serial);
                result[i, 0] = tmp[0];//valeur existante
                result[i, 1] = Math.Round(tmp[1],8);//valeur predite
                k ++;
                i++;
            }
            return result;
        }

        public double[] manyStepPrediction(int[] netArchitect, LinkedList<double> input, int indexOutput, double[,] w2, double[,] w3, Matrix<double> serial)
        {
            double[] v = new double[netArchitect[0] + netArchitect[1] + netArchitect[2]];
            double[] result = new double[2];
            for (int i = 0; i < netArchitect[0]; i++)
                v[i] = input.ElementAt(i);
            for (int i = 0; i < netArchitect[1]; i++)
                v[netArchitect[0] + i] = ActivationRule(w2, netArchitect[0], v, i, false, false); 
            int index = netArchitect[0] + netArchitect[1];
            v[index] = ActivationRule(w3, netArchitect[1], v, 0, true, true);
            result[0] = serial[indexOutput, 0];
            result[1] = v[index];
            return result;
        }
        public Matrix<double> manyStepPredictionProcess(int[] netArchitect, int initPrototype, int nbValPredict, Matrix<double> serial)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(nbValPredict, 2);
            double[] tmp = new double[2];
            int k = (initPrototype + netArchitect[0]) -1;
            int i = 0;
            LinkedList<double> input = new LinkedList<double>();
            for (int j = (initPrototype - 1); j < (initPrototype - 1 + netArchitect[0]); j++)
                input.AddLast(serial[j, 0]);
            while (k< ((initPrototype + netArchitect[0]) - 1 + nbValPredict))
            {
                tmp = manyStepPrediction(netArchitect, input, k, w2, w3, serial);
                input.AddFirst(tmp[1]);
                result[i, 0] = tmp[0];
                result[i, 1] = Math.Round(tmp[1],8);
                k++;
                i++;
            }
            return result;
        }

        public double[] CalculAllActivationRule(int[] netArchitect, int indexOutput, double[,] w2, double[,] w3, Matrix<double> serial, bool isPredict)
        {
            double[] v = new double[netArchitect[0] + netArchitect[1] + netArchitect[2]];
            for (int i = 0; i < netArchitect[0]; i++)
                v[i] = serial[(indexOutput - netArchitect[0]) + i, 0];
            for (int i = 0; i < netArchitect[1]; i++)
                v[netArchitect[0] + i] = ActivationRule(w2, netArchitect[0], v, i, false,false);
            int index = netArchitect[0] + netArchitect[1];
            v[index] = ActivationRule(w3, netArchitect[1], v, 0, true,isPredict);
            return v;
        }
    }
}