using UnityEngine;
using System.Collections;

using System.IO; // Para leitura e escrita em arquivos
using System; // Para usar o try / catch

public class Controlador : MonoBehaviour {

	public enum enumEstado{Jogando,Pausado,GameOver};
	public enumEstado Estado;
	public GameObject Player;
	static public Controlador singleton;

	public bool SavePoint;//aqui vamos saber se existe save point ativo ou nao

	//Arquivos a serem savos
	//INICIO

	public float tempo;
	public float tempoS;
	private int tempoM;
	private int tempoH;
	//FIM


	public int saveAtual;//aqui vai guardado a fase atual
	public string saveOrdem;//aqui fala qual save esta sendo usado

	public string nome = "charlles";
	public int pontos = 99;
	///


	public GameObject relogio;
	// Use this for initialization

	void Awake()
	{
		//CriaSaves();//cria os saves para evitar erro
	}


	void Start () {
		singleton = this;
		DontDestroyOnLoad(this);
		saveAtual = Application.loadedLevel;

	}
	
	// Update is called once per frame
	void Update () {
//		if(saveAtual != Application.loadedLevel)
//			saveAtual = Application.loadedLevel;

		//DEBUG DE LOAD/SAVE e save point
		if (Input.GetKey(KeyCode.Q)) 
		{
			SalvarJogo();
			Debug.Log("Salvo");
		}

		if (Input.GetKey(KeyCode.R)) 
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if (Input.GetKey(KeyCode.C)) //carrega proxima cena
		{
			if ((Application.loadedLevel+1) < Application.levelCount ) 
			{
				Application.LoadLevel(Application.loadedLevel+1);
				saveAtual++;
			}else
			{
				Debug.Log("nao existe proxima cena");
			}
		}










	}


	
	/////////////////////// LOAD/SAVE DO JOGO	//////////////////// 
	///LOAD
//	public void LoadJogo()
//	{
//		try 
//		{
//			using (BinaryReader reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Save.bin", FileMode.Open)))
//			{
//				
//				// Carrega do arquivo qual fase esta no momento salva
//
////				_int_saveAtual.GetComponent<TextMesh>().text = ""+reader.ReadInt32();//fase que parou de jogar
////				_txt_nome.GetComponent<TextMesh>().text  = ""+reader.ReadString();//nome do jogador que salvou
////				_int_pontos.GetComponent<TextMesh>().text  = ""+reader.ReadInt32();//pontos conseguidos
////				tempoS = reader.ReadSingle();
////				tempoM = reader.ReadInt32();
////				tempoH = reader.ReadInt32();
//				Debug.Log("SAVE ATUAL: "+nome);
//				
//				
//				
//				/*
//						// Outros comandos:
//						reader.ReadSingle();  // Le um float
//						reader.ReadBoolean(); // Le um boolean
//						reader.ReadInt32();   // Le um int
//						reader.ReadString();  // Le um string
//						reader.ReadChar();    // Le um char
//						*/					
//			}
//		}
//		// Se acontecer algum erro
//		catch (Exception e) 
//		{
//			Debug.Log("Erro ao ler o arquivo de save: ");
//			Debug.Log(e.Message);
//		}
//	}
	
	///SAVE
	public void SalvarJogo()
	{
		try 
		{
			// Application.persistentDataPath = C:\Users\SEU-LOGIN\AppData\LocalLow\DefaultCompany\NOME-DO-PROJETO-UNITY
			using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Save_"+saveOrdem+".bin", FileMode.Create)))
			{
				
				// Grava no arquivo A FASE ATUAL
				writer.Write(saveAtual);
				writer.Write(nome);
				writer.Write(pontos);
//				writer.Write(tempoS);
//				writer.Write(tempoM);
//				writer.Write(tempoH);
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
