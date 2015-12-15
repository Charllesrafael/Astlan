using UnityEngine;
using System.Collections;

using System.IO; // Para leitura e escrita em arquivos
using System; // Para usar o try / catch


public class Controlador : MonoBehaviour {


	//Arquivos a serem savos
		//INICIO
			public int saveAtual;//aqui vais er guardado a fase atual
		//FIM

	/////////////////////// LOAD/SAVE DO JOGO	//////////////////// 
		///LOAD
		public void LoadJogo()
		{
			try 
			{
				using (BinaryReader reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Save.bin", FileMode.Open)))
				{
					
					// Carrega do arquivo qual fase esta no momento salva
					saveAtual = reader.ReadInt32();
					
					Debug.Log("SAVE ATUAL: "+saveAtual);
					
					
					
					/*
						// Outros comandos:
						reader.ReadSingle();  // Le um float
						reader.ReadBoolean(); // Le um boolean
						reader.ReadInt32();   // Le um int
						reader.ReadString();  // Le um string
						reader.ReadChar();    // Le um char
						*/					
				}
			}
			// Se acontecer algum erro
			catch (Exception e) 
			{
				Debug.Log("Erro ao ler o arquivo de save: ");
				Debug.Log(e.Message);
			}
		}

	    ///SAVE
		public void SalvarJogo()
		{
			try 
			{
				// Application.persistentDataPath = C:\Users\SEU-LOGIN\AppData\LocalLow\DefaultCompany\NOME-DO-PROJETO-UNITY
				using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/save.bin", FileMode.Create)))
				{
					
					// Grava no arquivo A FASE ATUAL
					writer.Write(saveAtual);
					Debug.Log(writer);
				}
			}
			// Se acontecer algum erro
			catch (Exception e) 
			{
				Debug.Log("Erro ao gravar no arquivo de save: ");
				Debug.Log(e.Message);
			}
		}
}
