using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TP1
	{
	class Program
	{
		static void Main(string[] args)
			{
				Console.Out.WriteLine("Nombre de parametres" + args.Length);
				IniFile myininfile = new IniFile(args[0], args[1], args[2], args[3]);
				//Ouverture d'un ficher et lecture d'une valeur
				/*if(args.Length >=4){
					try{
						IniFile.Write(args[0], args[1], args[2], args[3]);
					}
					catch(ArgumentException Err)
					{
						Console.Out.WriteLine(Err.Message);
					}
					catch (FileNotFoundException Err)
					{
						Console.Out.WriteLine(Err.Message);
					}
					catch (Exception Err)
					{
						Console.Out.WriteLine(Err.Message);
					}
				}*/
				Console.In.Read();
		   }
	}
	}
