using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace PrimeNumbersGenerator
{
	class Generator
	{
		public Task<List<long>> GetPrimesSequential(long first, long last)
		{
			return Task.Run(() =>
			{
				List<long> primeNumbers = new List<long> { };
				if (first > last)
				{
					throw new InvalidDataException();
				}
				else
				{
					for (long i = first; i <= last; i++)
					{
						if (i <= 1)
						{
							continue;
						}
						else
						{
							bool isPrime = true;
							for (long j = 2; j < i; j++)
							{
								if (i % j == 0)
								{
									isPrime = false;
									break;
								}
							}
							if (isPrime)
							{
								primeNumbers.Add(i);
							}
						}
					}
					return primeNumbers;
				}
			});
		}

		public Task<List<long>> GetPrimesParallel(long first, long last)
		{
			return Task.Run(() =>
			{
				List<long> primeNumbers = new List<long> { };
				object primeNumbersLock = new object();

				if (first > last)
				{
					throw new InvalidDataException();
				}
				else
				{
					Parallel.ForEach(
						Partitioner.Create(first, last),
						() => new List<long>(),
						(range, loopState, localPrimeNumbers) => {
							for (long i = range.Item1; i < range.Item2; i++)
							{
								if (i <= 1)
								{
									continue;
								}
								else
								{
									bool isPrime = true;
									for (long j = 2; j < i; j++)
									{
										if (i % j == 0)
										{
											isPrime = false;
											break;
										}
									}
									if (isPrime)
									{
										localPrimeNumbers.Add(i);
									}
								}
							}
							return localPrimeNumbers;
						},
						(localPrimeNumbers) => {
							lock (primeNumbersLock)
							{
								primeNumbers.AddRange(localPrimeNumbers);
							}
						}
					);
					return primeNumbers;
				}
			});
		}
	}
}