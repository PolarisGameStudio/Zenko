using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDictionary : MonoBehaviour
{
	public static TutorialDictionary Instance;
	public static Dictionary<int,string[]> tutDic = new Dictionary<int,string[]>();
	public static bool initialized;
	public static Dictionary<int,string[]> randomTutDic = new Dictionary<int,string[]>();
	public static Dictionary<int,string[]> tutDicSpanish = new Dictionary<int,string[]>();



	public string[] tut_button = new string[]{"Rearrange the creature pieces to help me get to the green and red flower!", "Once I move, you can't change the pieces!", 
	"If you're still stuck try asking for a hint after restarting the level"};
    public string[] tut_1 = new string[]{"Hi!, My name is Zenko", "Help me get to that flower tunnel by sliding your finger through the screen."};
	public string[] tut_2 = new string[]{"That moving piece over there is Pedro", "Drag him into the right tile so I can use him as a wall", "drag him before moving me to the flower tunnel!"};
	//string[] tut_2 = new string[]{"This place is a mess! I can't get to my goal like this", "Touch the pieces and arrange them on the board", "BEFORE moving me to the flower!"};
	public string[] tut_3 = new string[]{"I wouldn't want to end in that hole over there!" };
	public string[] tut_6 = new string[]{"Make sure both Pedros are on the right place for me to get to the flower"};
	public string[] tut_9 = new string[]{"You will only get 3 stars if you do the least amount of moves possible.", "Those dots next to the bar on the right represent the number of best moves"};
	public string[] tut_12 = new string[]{"That yellow flower just started to grow!","That must mean a new spring perhaps isn't that far away", "Don't put anything over it!, better to preserve them"};
    public string[] tut_41 = new string[]{"Those birds are called Icarus", "They send a strong air current in the tile right in front of them", "If I go near It'll change my direction!"};
    public string[] tut_50 = new string[]{"That's a fragile tile", "It'll fall quickly, but don't worry, I can outrun them"};
	public string[] tut_52 = new string[]{"Make sure not to linger on one of those fragile tiles or I'll fall", "I can go through them, but not stop on them"};				
    public string[] tut_55 = new string[]{"I'm pretty sure Icarus can blow air faster than that fragile tile can fall"};
    public string[] tut_68 = new string[]{"I don't always need the wind to get to the flower!"};
    public string[] tut_81 = new string[]{"That bag with the circle becomes Pedro once I go over it!"};
    public string[] tut_121 = new string[]{"That bag with the arrow has an Icarus sleeping inside!", "I'll wake it once I go over it."};
    public string[] tut_122 = new string[]{"You can tell by the arrow in the bag, the direction it'll blow when it wakes!"};
	public string[] tut_160 = new string[]{"This is the end of our adventure for now", "Be sure to come back soon for more levels and new friends!"};
	
    //Choose one of the dialogs to happen after 30s of innactivity

    public string[] random1 = new string[] {"Focus! You can do it!"};
    public string[] random2 = new string[] {"If you have trouble arranging the pieces, press that button with a bulb over there!"};
    public string[] random3 = new string[] {"If you're stuck, Restart!"};
    //Choose one of the dialogs to happen after 5 consecutive deaths
    public string[] random4 = new string[] {"You can restart the level and ask for a hint once I'm back in my starting position"};
    public string[] random5 = new string[] {"We almost made it! Keep trying!"};
    public string[] random6 = new string[] {"This time we'll make it, I'm sure!"};


	public string[] music = new string[] {"Music"};
	public string[] sfx = new string[] {"SFX"};

	public string[] buyMenu = new string[]{"Disable Ads?", "No" , "Yes", "-One-time payment\n-Removes all ads\n-Unlimited hints\n-Unlocks past Puzzles of the Day"};
	public string[] hint = new string[]{"Hint", "Watch an Ad\nto get a hint", "No Ads +\nUnlinmited hints"};
//Spanish Assets SP

	public string[] tut_button_sp = new string[]{"¡Acomoda las criaturas y ayúdame a llegar a la flor roja!", "Una vez que me muevo, ¡no podrás cambiar las piezas de lugar!", "Si no logras resolver el nivel, pide una pista después de reiniciarlo"};
    public string[] tut_1_sp = new string[]{"¡Hola!, mi nombre es Zenko", "Ayúdame a llegar a la flor roja deslizando tu dedo por la pantalla"};
	public string[] tut_2_sp = new string[]{"Esa pieza se llama Pedro", "Puedes moverla por el tablero para poder utilizarlo como barrera", "¡Acomódalo antes de moverme hacia la flor roja!"};
	public string[] tut_3_sp = new string[]{"¡No me gustaría terminar en ese agujero!"};
	public string[] tut_6_sp = new string[]{"Asegúrate de que ambos Pedro estén en el lugar correcto ANTES de moverme hacia la flor"};
	public string[] tut_9_sp = new string[]{"Solo obtendrás las 3 estrellas si solucionas el tablero en la menor cantidad de movimientos posibles.", "Los puntos al lado de la barra representan la menor cantidad de movimientos posibles"};
	public string[] tut_12_sp = new string[]{"¡Esa pequeña flor amarilla está creciendo!", "Eso significa que la primavera está cerca", "¡No pongas nada encima, hay que conservarlas!"};
	public string[] tut_41_sp = new string[]{"Esos pájaros se llaman Icarus", "Envían una fuerte corriente de aire frente a ellos", "Si paso frente a ellos, ¡cambiarán mi dirección!"};
	public string[] tut_50_sp = new string[]{"Eso es piso frágil", "Se destruye rápidamente... pero yo soy más rápido"};
	public string[] tut_52_sp = new string[]{"Recuerda no detenerme sobre el piso frágil, o me caeré", "Puedo pasar sobre ellas, pero no soportan mi peso al detenerme"};				
	public string[] tut_55_sp = new string[]{"Tengo la certeza de que el aire de Icarus es más rápido que el suelo frágil"};
	public string[] tut_68_sp = new string[]{"¡No siempre necesito aire para llegar a la flor!"};
	public string[] tut_81_sp = new string[]{"¡Esa bolsa con el círculo se transforma en Pedro al pasar sobre ella!"};
	public string[] tut_121_sp = new string[]{"¡Dentro de esa bolsa con la flecha está Icarus dormido!" , "Lo despertaré al pasar encima"};
	public string[] tut_122_sp = new string[]{"La dirección de la flecha en la bolsa, ¡es la misma dirección en la Icarus soplará al despertar!"};
	public string[] tut_160_sp = new string[]{"Aquí termina nuestra aventura por ahora", "¡Recuerda regresar pronto para jugar en nuevos niveles y conocer nuevos amigos!"};
	
    //Choose one of the dialogs to happen after 30s of innactivity

    public string[] random1_sp = new string[] {"¡Concéntrate!¡Tú puedes!"};
    public string[] random2_sp = new string[] {"Si llegas a tener problemas acomodando las piezas, ¡presiona el botón con el foco!"};
    public string[] random3_sp = new string[] {"Cuando no encuentres la respuesta, ¡vuelve a empezar!"};
    //Choose one of the dialogs to happen after 5 consecutive deaths
    public string[] random4_sp = new string[] {"Puedes reiniciar el nivel y pedir una pista en cuanto regrese a mi posición inicial"};
    public string[] random5_sp = new string[] {"¡Estuvimos a punto de lograrlo! ¡Sigue intentando!"};
    public string[] random6_sp = new string[] {"¡Esta vez sí lo vamos a lograr!"};


	public string[] music_sp = new string[] {"Música"};
	public string[] sfx_sp = new string[] {"Efectos de sonido"};

	public string[] buyMenu_sp = new string[]{"Desactivar Anuncios?", "No", "Si", "-Paga solo una vez\n-No mas anuncios\n-Pistas ilimitadas\n-Desbloquea niveles diarios pasados"};
	
	public string[] hint_sp = new string[]{"Pista", "Ver anuncio para\npedir una pista", "No anuncios +\npistas ilimitadas"};
	//yo lo dejaria como sfx
	//string[] sfx_sp = new string[] {"SFX"};


	void Awake(){
		Instance = this;
		if (!initialized)
		PopulateDictionary();
		initialized = true;
	}

	public void PopulateDictionary(){
		tutDic.Add(0,tut_button);
		tutDic.Add(1,tut_1);
		tutDic.Add(2,tut_2);
		tutDic.Add(3,tut_3);
		tutDic.Add(6,tut_6);
		tutDic.Add(9,tut_9);
		tutDic.Add(12,tut_12);
		tutDic.Add(41,tut_41);
		tutDic.Add(50,tut_50);
		tutDic.Add(52,tut_52);
		tutDic.Add(55,tut_55);
		tutDic.Add(68,tut_68);
		tutDic.Add(81,tut_81);
		tutDic.Add(121,tut_121);
		tutDic.Add(122,tut_122);
		tutDic.Add(160,tut_160);

		tutDicSpanish.Add(0,tut_button_sp);
		tutDicSpanish.Add(1,tut_1_sp);
		tutDicSpanish.Add(2,tut_2_sp);
		tutDicSpanish.Add(3,tut_3_sp);
		tutDicSpanish.Add(6,tut_6_sp);
		tutDicSpanish.Add(9,tut_9_sp);
		tutDicSpanish.Add(12,tut_12_sp);
		tutDicSpanish.Add(41,tut_41_sp);
		tutDicSpanish.Add(50,tut_50_sp);
		tutDicSpanish.Add(52,tut_52_sp);
		tutDicSpanish.Add(55,tut_55_sp);
		tutDicSpanish.Add(68,tut_68_sp);
		tutDicSpanish.Add(81,tut_81_sp);
		tutDicSpanish.Add(121,tut_121_sp);
		tutDicSpanish.Add(122,tut_122_sp);
		tutDicSpanish.Add(160,tut_160_sp);
	}



}
