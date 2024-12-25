
using System;
namespace proiect_ReteleNeuronaleAplicatii
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Operatie dorita? Executie exemplu concret sau generare parametri retea? (generare/exemplu/exit):");
            string? optiune = Console.ReadLine();

            while (true)
            {
                if (optiune == "exemplu")
                {
                    double[] inputs = new double[] { 0.48, 0.14, 0.98, 0.82 };

                    double[] expectedOutputs = new double[] { 0.62, 0.21 };

                    double learningRate = 0.71;

                    double maxError = 0.01;

                    double[,] inputToHiddenWeights = new double[4, 3]
                    {
                    { 0.17, 0.08, -0.76 },
                    { 0.32, 0.99, 0.43 },
                    { 0.22, 0.98, 0.94 },
                    { 0.71, 0.57, -0.91 }
                    };

                    double[,] hiddenToOutputWeights = new double[3, 2]
                     {
                     { 0.21, 0.29 },
                     { 0.29, 0.66 },
                     { -0.44, -0.48 }
                     };

                    double[] hiddenLayerOutputs = new double[3];
                    double[] outputLayerOutputs = new double[2];

                    double[] hiddenLayerErrors = new double[3];
                    double[] outputLayerErrors = new double[2];

                    NeuralNetwork nn = new(inputToHiddenWeights, hiddenToOutputWeights, hiddenLayerOutputs, outputLayerOutputs, hiddenLayerErrors, outputLayerErrors, learningRate, maxError);
                    nn.Train(inputs, expectedOutputs);

                    double[] result = nn.Test(inputs);
                    Console.WriteLine("Iesirea pentru intrarea [0.125, 0.25, 0.5, 1]:");
                    foreach (var val in result)
                    {
                        Console.WriteLine(val);
                    }
                }
                else if (optiune == "generare")
                {
                    GeneratorDate generatorDate = new();
                    generatorDate.GenerareParametri(4, 3, 2);
                }
                else if (optiune == "exit")
                {
                    Environment.Exit(0);
                }
                Console.Write("Operatie dorita? Executie exemplu concret sau generare parametri retea? (generare/exemplu/exit):");
                optiune = Console.ReadLine();
            }
        }
    }
}