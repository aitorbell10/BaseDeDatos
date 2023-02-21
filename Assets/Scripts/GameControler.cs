using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{

	public GameObject clicker;
	private User usuario;
	private DatabaseHandler db;

	public Text marcador;
	public Text mensajeExito;

	public Button clikerBtn;
	public Button registerBtn;
	public Button inicioSesionBtn;
	public Button guardarBtn;

	public InputField nicknameField;
	public InputField passwordField;

	void Start()
	{
		db = GetComponent<DatabaseHandler>();
		clikerBtn.onClick.AddListener(SumarPuntos);
		registerBtn.onClick.AddListener(Registrarse);
		inicioSesionBtn.onClick.AddListener(IniciarSesion);
		guardarBtn.onClick.AddListener(Guardar);
	}

	void SumarPuntos()
	{
		usuario.Puntuacion++;
		marcador.text = usuario.Puntuacion.ToString();
		
	}

	void Registrarse()
	{
		if (nicknameField.text.Equals("") || passwordField.text.Equals(""))
		{
			mensajeExito.text = "Introduce un usuario y contraseña";
			return;
		}
		bool resultado = db.Registrar(nicknameField.text, passwordField.text);
		if(resultado)
        {
			mensajeExito.text = "Usuario registrado, inicie sesión";
        }
		else
        {
			mensajeExito.text = "Usuario ya registrado";
		}

	}

	void IniciarSesion()
    {
		usuario = db.IniciarSesion(nicknameField.text, passwordField.text);
		if(usuario != null)
        {
			clicker.SetActive(true);
			mensajeExito.text = "Has iniciado sesión como " + usuario.Nombre;
			marcador.text = usuario.Puntuacion.ToString();
		}
		else
        {
			mensajeExito.text = "Usuario o contraseña incorrecto";
        }

    }

	void Guardar()
    {

		db.GuardarDatosDB(usuario);
		db.GuardarJSON(usuario);
		mensajeExito.text = "Guardado con éxito";
	}
	
}
