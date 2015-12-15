using UnityEngine;
using System.Collections;

using System.IO; // Para leitura e escrita em arquivos
using System; // Para usar o try / catch

public class Salve : MonoBehaviour {

	public string Ordem;
	public int fa;//fase atual

	public GameObject int_saveAtual;
	public GameObject txt_nome;
	public GameObject int_pontos;
//	public GameObject XJ;

	// Use this for initialization
	void Start () {
		LoadJogo();
//		int_saveAtual.GetComponent<TextMesh>().text = ""+saveAtual;
//		txt_nome.GetComponent<TextMesh>().text = nome;
//		int_pontos.GetComponent<TextMesh>().text = ""+pontos;
//		int_saveAtual.GetComponent<TextMesh>().text
	}


//	public void OnMouseOver()
//	{
//		if (Input.GetMouseButtonDown(0))
//		{
//			Debug.Log(Ordem);
//			Controlador.singleton.saveOrdem = Ordem;
//			if (fa != -1) 
//			{
//				Application.LoadLevel(fa);
//			}else
//			{
//				Application.LoadLevel(1);
//			}
//
//		}
//	}



	public void NovoJogo()
	{
		try 
		{
			// Application.persistentDataPath = C:\Users\SEU-LOGIN\AppData\LocalLow\DefaultCompany\NOME-DO-PROJETO-UNITY
			using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Save_"+Ordem+".bin", FileMode.Create)))
			{

				writer.Write(-1);
				writer.Write("");
				writer.Write(0);
				
				txt_nome.GetComponent<TextMesh>().text  = "\n\nNOVO JOGO";
				int_saveAtual.GetComponent<TextMesh>().text = "";
				int_pontos.GetComponent<TextMesh>().text  = "";
				
			}
			
		}
		// Se acontecer algum erro
		catch (Exception e) 
		{
			Debug.Log("Erro ao gravar no arquivo de save: ");
			Debug.Log(e.Message);
		}
	}

	public void LoadJogo()
	{
		try 
		{
			using (BinaryReader reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Save_"+Ordem+".bin", FileMode.Open)))
			{
				int p;
				string n;
				// Carrega do arquivo qual fase esta no momento salva
				
				fa = reader.ReadInt32();//fase que parou de jogar
				n  = reader.ReadString();//nome do jogador que salvou
				p  = reader.ReadInt32();//pontos conseguidos

				Debug.Log("SAVE ATUAL: "+Ordem);
				
				if (fa < 0) {
					txt_nome.GetComponent<TextMesh>().text  = "\n\nNOVO JOGO";
					int_saveAtual.GetComponent<TextMesh>().text = "";
					int_pontos.GetComponent<TextMesh>().text  = "";
//					XJ.SetActive(false);
				}else
				{
					int_saveAtual.GetComponent<TextMesh>().text = "Fase atual: \n"+fa;//fase que parou de jogar
					txt_nome.GetComponent<TextMesh>().text  = n;//nome do jogador que salvou
					int_pontos.GetComponent<TextMesh>().text  = "Pontos:\n"+p;//pontos conseguidos
//					XJ.SetActive(true);
				}
				
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
			using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Save_"+Ordem+".bin", FileMode.Create)))
			{
				
				writer.Write(-1);
				writer.Write("");
				writer.Write(0);
				
				txt_nome.GetComponent<TextMesh>().text  = "\n\nNOVO JOGO";
				int_saveAtual.GetComponent<TextMesh>().text = "";
				int_pontos.GetComponent<TextMesh>().text  = "";
				
			}
			Debug.Log("foi criado novo arquivo");
			Debug.Log(e.Message);
		}
	}




	// Update is called once per frame
	void Update () 
	{
	


	}

}
