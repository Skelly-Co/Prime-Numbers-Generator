using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrimeNumbersGenerator {
	class Program {
		static void Main(string[] args) {
			Generator generator = new Generator();
			Time(() => {
				List<long> primeNumbers = generator.GetPrimesSequential(1, 1_000_000);
				Console.WriteLine(primeNumbers.Count);
			}, "PrimeNumbersSequential");

			Time(() => {
				List<long> primeNumbers = generator.GetPrimesSequential(1, 1_000_000);
				Console.WriteLine(primeNumbers.Count);
			}, "PrimeNumbersParallel");
		}

		static void Time(Action action, String taskName) {
			Stopwatch watch = Stopwatch.StartNew();
			action.Invoke();
			watch.Stop();
			Console.WriteLine(taskName + " - Time elapsed: " + watch.ElapsedMilliseconds);
		}
	}
}
