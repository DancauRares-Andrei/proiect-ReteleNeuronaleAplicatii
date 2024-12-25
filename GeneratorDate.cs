using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect_ReteleNeuronaleAplicatii
{
    public class GeneratorDate
    {
        readonly Random rand = new();
        public void GenerareParametri(int inputSize,int hiddenSize,int outputSize)
        {
            double[] inputs = new double[inputSize];
            for (int i = 0; i < inputSize; i++)
            {
                inputs[i] = double.Round(rand.NextDouble(),2);
            }
            double[] expectedOutputs = new double[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                expectedOutputs[i] = double.Round(rand.NextDouble(),2);
            }
            double learningRate = double.Round(rand.NextDouble() * (1.0 - 0.01) + 0.01,2);
            double maxError = double.Round(rand.NextDouble() * (0.1 - 0.001) + 0.001,2);

            double[,] inputToHiddenWeights= new double[inputSize, hiddenSize]; ;
            double[,] hiddenToOutputWeights= new double[hiddenSize, outputSize];
            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < hiddenSize; j++)
                {
                    inputToHiddenWeights[i, j] = double.Round(rand.NextDouble() * 2 - 1,2); 
                }
            }
            for (int j = 0; j < hiddenSize; j++)
            {
                for (int k = 0; k < outputSize; k++)
                {
                    hiddenToOutputWeights[j, k] = double.Round(rand.NextDouble() * 2 - 1,2); 
                }
            }
            Console.WriteLine("Valori generate aleatoriu:");
            Console.WriteLine("Intrari: " + string.Join(", ", inputs));
            Console.WriteLine("Iesiri dorite: " + string.Join(", ", expectedOutputs));
            Console.WriteLine($"Rata de invatare: {learningRate:F2}");
            Console.WriteLine($"Eroare maxima: {maxError:F2}");
            Console.WriteLine($"Ponderi intre intrare si stratul ascuns:");
            for (int i = 0; i < inputToHiddenWeights.GetLength(0); i++)
            {
                for (int j = 0; j < inputToHiddenWeights.GetLength(1); j++)
                {
                    Console.Write($"{inputToHiddenWeights[i, j]:F5} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Ponderi intre stratul ascuns si iesire:");
            for (int i = 0; i < hiddenToOutputWeights.GetLength(0); i++)
            {
                for (int j = 0; j < hiddenToOutputWeights.GetLength(1); j++)
                {
                    Console.Write($"{hiddenToOutputWeights[i, j]:F5} ");
                }
                Console.WriteLine();
            }
        }
    }
}
