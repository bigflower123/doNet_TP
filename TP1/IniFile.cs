using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace TP1
	{
	class IniFile
		{
			private ArrayList list = new ArrayList();

			public IniFile(string Filename, string Section, string Name, string Value)
			{
				ReadFile(Filename);
				SupprimeSection(Section);
				WriteFile(Filename);
			}

			/// <summary>
			/// Lecture du ficher de win.ini dans un ArrayList 
			/// </summary>
			/// <param name="Filename"></param>
			private void ReadFile(string Filename){
				StreamReader Sr = File.OpenText(Filename);
				string Line;
				while((Line = Sr.ReadLine())!=null){
					list.Add(Line);
				}
				Sr.Close();
				Sr.Dispose();
			}

			/// <summary>
			/// Ecriture list dans la file
			/// </summary>
			/// <param name="Filename"></param>
			public void WriteFile(string Filename)
			{
				if (File.Exists(Filename))
					{
					using (StreamWriter Sw = new StreamWriter(Filename, false))
						{
						foreach (string str in list)
							{
							Sw.WriteLine(str);
							}
						}
					}
			}

			/// <summary>
			/// Si le section n'existe pas, on l'ajoute
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <param name="Value"></param>
			private void AjouteSection(string Section, string Name, string Value){
				int myIndex = list.IndexOf("["+Section.Trim()+"]");
				if(myIndex != -1){
					Console.Out.WriteLine("La section a déjà existe");
				}else
				{
					list.Add("["+Section+"]");
					list.Add(Name+"="+Value);
				}
			}
            
			/// <summary>
			/// Supprimer la section
			/// </summary>
			/// <param name="Section"></param>
			private void SupprimeSection(string Section)
			{
				int myIndex = list.IndexOf("[" + Section.Trim() + "]");
				list.RemoveAt(myIndex);
				while (myIndex < list.Count && !list[myIndex].ToString().StartsWith("["))
				{
					list.RemoveAt(myIndex);
				}
			}

			private string GetString(string Section, string Name)
			{
				return GetString(Section, Name, null);
			}

			/// <summary>
			/// Recherche la valeur correspondante aux Name
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <param name="Default"></param>
			/// <returns></returns>
			private string GetString(string Section, string Name, string Default)
			{
				int myIndex = list.IndexOf("["+Section.Trim()+"]");    //Supprimer le espace vide
				if(myIndex == -1){
					Console.Out.WriteLine("Section n'existe pas");
					return null;
				}
				myIndex++;
				while( myIndex < list.Count && !list[myIndex].ToString().StartsWith("[")){
					string RegexTest = "=";
					string[] tmp;
					tmp = Regex.Split(list[myIndex].ToString(), RegexTest);
					if(tmp[0].Trim() == Name){    //Supprimer l'espace vide de Name
						return tmp[1].Trim();	  //Supprimer l'espace vide de Value
					}
					myIndex++;
				}
				return null;
            }

			private static int GetInteger(string Filename, string Section, string Name)
			{
				return IniFile.GetInteger( Filename, Section, Name);
			}

			private static string GetInteger(string Filename, string Section, string Name, int Default)
			{
				throw new NotImplementedException();
			}


			/// <summary>
			/// Rechercher la position d'un Name
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <returns></returns>
			private int RecherchePosition(string Section, string Name)
			{
				int myIndex = list.IndexOf("[" + Section.Trim() + "]");    //Supprimer le espace vide
				if (myIndex == -1)
				{
					Console.Out.WriteLine("Section n'existe pas");
					return -1;
				}
				myIndex++;
				while (myIndex < list.Count && !list[myIndex].ToString().StartsWith("["))
				{
					string RegexTest = "=";
					string[] tmp;
					tmp = Regex.Split(list[myIndex].ToString(), RegexTest);
					if (tmp[0] == Name)
					{
						return myIndex;
					}
					myIndex++;
				}
				return -1;
			}


			/// <summary>
			/// Modifier la valeur d'un nom
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <param name="Value"></param>
			private void ModifieValue(string Section, string Name, string Value)
			{
				string ValueRecherche = GetString(Section, Name, null);
				if(ValueRecherche == Value){
					Console.Out.WriteLine("La valeur déjà existe");
				}else{
					int PostionLigne = RecherchePosition(Section, Name);   //La ligne qu'on recherche
					if(PostionLigne == -1){
						Console.Out.WriteLine(Name + " Non trouvé");
					}else{
						string str = Name + "=" + Value;
						list[PostionLigne] = str;
					}
					
				}
			}


			/// <summary>
			/// Ajouter une ligne name, value, après la section correspondante
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <param name="Value"></param>
			private void AjouteValue(string Section, string Name, string Value)
			{
				string ValueRecherche = GetString(Section, Name, null);
				if (ValueRecherche == Value)
				{
					Console.Out.WriteLine("La valeur déjà existe");
				}else
				{
					int myIndex = list.IndexOf("[" + Section.Trim() + "]");  //Recherche la position de section
					string str = Name+"="+Value;
					list.Insert(++myIndex, str);
				}
			}

			/// <summary>
			/// Supprimer la ligne de Name=Value
			/// </summary>
			/// <param name="Section"></param>
			/// <param name="Name"></param>
			/// <param name="Value"></param>
			private void SupprimeValue(string Section, string Name, string Value)
			{
				string ValueRecherche = GetString(Section, Name, null);
				if (ValueRecherche != Value)
				{
					Console.Out.WriteLine("Il n'y a pas de cette ligne");
				}
				else
				{
					int PostionLigne = RecherchePosition(Section, Name);   //La ligne qu'on recherche
					list.RemoveAt(PostionLigne);
				}
			}


			/// <summary>
			/// Ecrit une valeur dans le ficher ini
			/// </summary>
			/// <param name="Filename">Nom et chemin du ficher ini.</param>
			/// <param name="Section">Nom de la section.</param>
			/// <param name="Name">Nom de la valeur.</param>
			/// <param name="Value">Contenue de la valeur.</param>
			/// <exception cref="ArgumentException">...</exception>
			/// <exception cref="FileNotFoundException">...</exception>
			public static void Write(string Filename, string Section, string Name, object Value)
			{
				if( string.IsNullOrEmpty(Filename)){
					throw new ArgumentException("Filename est vide");
				}
				if (string.IsNullOrEmpty(Section))
				{
					throw new ArgumentException("Section est vide");
				}
				if (string.IsNullOrEmpty(Name))
				{
					throw new ArgumentException("Name est vide");
				}
				if(!File.Exists(Filename))
				{
					throw new FileNotFoundException("Fichier non trouvé");
				}
				if(Value is string){
					WriteString( Filename, Section, Name, (string)Value);
				}else{
					WriteString(Filename, Section, Name, Value.ToString());
				}
			}

			public static void WriteString(string Filename, string Section, string Name, string Value)
			{
			}

			public static void WriteInteger(string Filename, string Section, string Name, int Value)
			{
			}

		
		}
	}
