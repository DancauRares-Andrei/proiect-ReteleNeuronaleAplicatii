using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect_ReteleNeuronaleAplicatii
{
    public class NeuralNetwork
    {
        private double[,] inputToHiddenWeights;
        private double[,] hiddenToOutputWeights;
        private double[] hiddenLayerOutputs;
        private double[] outputLayerOutputs;
        private double[] hiddenLayerErrors;
        private double[] outputLayerErrors;

        private double learningRate;
        private double maxError;

        public NeuralNetwork(double [,] inputToHiddenWeights, double[,] hiddenToOutputWeights, double[] hiddenLayerOutputs, double[] outputLayerOutputs, double[] hiddenLayerErrors, double[] outputLayerErrors, double learningRate, double maxError)
        {
            this.inputToHiddenWeights = inputToHiddenWeights;
            this.hiddenToOutputWeights = hiddenToOutputWeights;
            this.hiddenLayerOutputs = hiddenLayerOutputs;
            this.outputLayerOutputs = outputLayerOutputs;
            this.hiddenLayerErrors = hiddenLayerErrors;
            this.outputLayerErrors = outputLayerErrors;
            this.learningRate = learningRate;
            this.maxError = maxError;
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }


        private double SigmoidDerivative(double x)
        {
            return x * (1 - x);
        }

        public void FeedForward(double[] inputs)
        {
            Console.WriteLine("\nStrat ascuns");
            for (int j = 0; j < hiddenLayerOutputs.Length; j++)
            {
                double sum = inputs[0] * inputToHiddenWeights[0, j];
                Console.Write($"o{j + 1 + inputs.Length}=f(w1{j + 1 + inputs.Length}*o1");
                string s = $"o{j + 1 + inputs.Length}=f(" + $"({double.Round(inputToHiddenWeights[0, j], 3)})({double.Round(inputs[0], 3)})";
                for (int i = 1; i < inputs.Length; i++)
                {
                    sum += inputs[i] * inputToHiddenWeights[i, j];
                    Console.Write($"+w{i + 1}{j + 1 + inputs.Length}*o{i + 1}");
                    s += $"+({double.Round(inputToHiddenWeights[i, j], 3)})({double.Round(inputs[i], 3)})";
                }

                Console.WriteLine(")");
                hiddenLayerOutputs[j] = Sigmoid(sum);
                s += $")=f({double.Round(sum, 5)})";
                Console.WriteLine(s);
                Console.WriteLine($"o{j + 1 + inputs.Length}={double.Round(hiddenLayerOutputs[j], 5)}");
            }

            Console.WriteLine("\nStratul de iesire");
            for (int k = 0; k < outputLayerOutputs.Length; k++)
            {
                double sum = hiddenLayerOutputs[0] * hiddenToOutputWeights[0, k];
                Console.Write($"o{k + 1 + inputs.Length + hiddenLayerOutputs.Length}=f(w{1 + inputs.Length}{k + 1 + hiddenLayerOutputs.Length + inputs.Length}*o{1 + inputs.Length}");
                string s = $"o{k + 1 + hiddenLayerOutputs.Length + inputs.Length}=f(({double.Round(hiddenToOutputWeights[0, k], 3)})({double.Round(hiddenLayerOutputs[0], 3)})";
                for (int j = 1; j < hiddenLayerOutputs.Length; j++)
                {
                    sum += hiddenLayerOutputs[j] * hiddenToOutputWeights[j, k];
                    Console.Write($"+w{j + 1 + inputs.Length}{k + 1 + hiddenLayerOutputs.Length + inputs.Length}*o{j + 1 + inputs.Length}");
                    s += $"+({double.Round(hiddenToOutputWeights[j, k], 3)})({double.Round(hiddenLayerOutputs[j], 3)})";
                }
                Console.WriteLine(")");
                s += $")=f({double.Round(sum, 5)})";
                Console.WriteLine(s);
                outputLayerOutputs[k] = Sigmoid(sum);
                Console.WriteLine($"o{k + 1 + hiddenLayerOutputs.Length + inputs.Length}={double.Round(outputLayerOutputs[k], 5)}");
            }
        }

        public void Backpropagate(double[] inputs, double[] expectedOutputs)
        {
            Console.WriteLine("\nStratul de iesire");
            for (int k = 0; k < outputLayerErrors.Length; k++)
            {
                int curent = k + 1 + hiddenLayerOutputs.Length + inputs.Length;
                outputLayerErrors[k] = (expectedOutputs[k] - outputLayerOutputs[k]) * SigmoidDerivative(outputLayerOutputs[k]);
                Console.WriteLine($"D{curent}=(d{curent}-o{curent})*o{curent}*(1-o{curent})");
                Console.WriteLine($"D{curent}=(({double.Round(expectedOutputs[k], 5)}-{double.Round(outputLayerOutputs[k], 5)})*({double.Round(outputLayerOutputs[k], 5)})*(1-{double.Round(outputLayerOutputs[k], 5)}))");
                Console.WriteLine($"D{curent}={double.Round(outputLayerErrors[k], 5)}");
            }

            Console.WriteLine("\nStratul ascuns");
            for (int j = 0; j < hiddenLayerErrors.Length; j++)
            {
                int curent = j + 1 + inputs.Length;
                string s = $"D{curent}=(";
                string s1 = $"D{curent}=(";
                double error = 0;
                for (int k = 0; k < outputLayerErrors.Length; k++)
                {
                    s += $"D{k + 1 + inputs.Length + hiddenLayerOutputs.Length}*w{curent}{k + 1 + inputs.Length + hiddenLayerOutputs.Length}+";
                    s1 += $"({double.Round(outputLayerErrors[k], 5)})*({double.Round(hiddenToOutputWeights[j, k], 5)})+";
                    error += outputLayerErrors[k] * hiddenToOutputWeights[j, k];
                }
                s = s.Substring(0, s.Length - 1) + $")*o{curent}*(1-o{curent})";
                s1 = s1.Substring(0, s1.Length - 1) + $")*({double.Round(hiddenLayerOutputs[j], 5)})*(1-({double.Round(hiddenLayerOutputs[j], 5)}))";
                Console.WriteLine(s);
                Console.WriteLine(s1);
                hiddenLayerErrors[j] = error * SigmoidDerivative(hiddenLayerOutputs[j]);
                Console.WriteLine($"D{curent}={double.Round(hiddenLayerErrors[j], 5)}");
            }
            Console.WriteLine("\nValorile delta:");

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < hiddenLayerOutputs.Length; j++)
                {
                    inputToHiddenWeights[i, j] += learningRate * hiddenLayerErrors[j] * inputs[i];
                    Console.WriteLine($"dw{i + 1}{j + 1 + inputs.Length}=n*D{j + 1 + inputs.Length}*o{i + 1}=({learningRate})({double.Round(hiddenLayerErrors[j], 5)})({double.Round(inputs[i], 5)})={double.Round(learningRate * hiddenLayerErrors[j] * inputs[i], 5)}");
                }
            }
            Console.WriteLine();

            for (int j = 0; j < hiddenLayerOutputs.Length; j++)
            {
                for (int k = 0; k < outputLayerOutputs.Length; k++)
                {
                    hiddenToOutputWeights[j, k] += learningRate * outputLayerErrors[k] * hiddenLayerOutputs[j];
                    Console.WriteLine($"dw{j + 1 + inputs.Length}{k + 1 + inputs.Length + hiddenLayerOutputs.Length}=n*D{k + 1 + inputs.Length + hiddenLayerOutputs.Length}*o{j + 1 + inputs.Length}=({learningRate})({double.Round(outputLayerErrors[k], 5)})({double.Round(hiddenLayerOutputs[j], 5)})={double.Round(learningRate * outputLayerErrors[k] * hiddenLayerOutputs[j], 5)}");
                }
            }

        }

        public void Train(double[] inputs, double[] expectedOutputs)
        {
            double totalError = 1.0;
            int epoch = 0;

            while (totalError > maxError)
            {
                epoch++;
                Console.WriteLine("Iteratia " + epoch + ":");
                Console.WriteLine("\nPropagarea inainte");
                FeedForward(inputs);
                Console.WriteLine("\nPropagarea inapoi");
                Backpropagate(inputs, expectedOutputs);

                totalError = 0;
                for (int i = 0; i < expectedOutputs.Length; i++)
                {
                    totalError += Math.Pow(expectedOutputs[i] - outputLayerOutputs[i], 2);
                }

                DisplayWeights(epoch);
                Console.WriteLine();
            }
            Console.WriteLine($"\nAntrenarea s-a terminat cu eroarea: {totalError} Epoca:{epoch}");
        }
        public void DisplayWeights(int epoch)
        {
            Console.WriteLine($"\nWih:");
            for (int i = 0; i < inputToHiddenWeights.GetLength(0); i++)
            {
                for (int j = 0; j < inputToHiddenWeights.GetLength(1); j++)
                {
                    Console.Write($"{inputToHiddenWeights[i, j]:F5} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nWho:");
            for (int i = 0; i < hiddenToOutputWeights.GetLength(0); i++)
            {
                for (int j = 0; j < hiddenToOutputWeights.GetLength(1); j++)
                {
                    Console.Write($"{hiddenToOutputWeights[i, j]:F5} ");
                }
                Console.WriteLine();
            }
        }

        public double[] Test(double[] inputs)
        {
            FeedForward(inputs);
            return outputLayerOutputs;
        }
    }
}
