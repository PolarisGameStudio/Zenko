// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TutorialDictionary : MonoBehaviour
// {
// 	public static TutorialDictionary Instance;
// 	public static Dictionary<int,string[]> tutDic = new Dictionary<int,string[]>();
// 	public static bool initialized;
// 	public static Dictionary<int,string[]> randomTutDic = new Dictionary<int,string[]>();

// 	string[] tut_button = new string[]{"Rearrange the creature pieces to help me get to the green and red flower!", "Once I move, you can't change the pieces!", 
// 	"If you're still stuck try asking for a hint after restarting the level"};
//     string[] tut_1 = new string[]{"Hi!, My name is Zenko", "Help me get to that flower tunnel by sliding your finger through the screen."};
// 	string[] tut_2 = new string[]{"That moving piece over there is Pedro", "Drag him into the right tile so I can use him as a wall", "drag him before moving me to the flower tunnel!"};
// 	//string[] tut_2 = new string[]{"This place is a mess! I can't get to my goal like this", "Touch the pieces and arrange them on the board", "BEFORE moving me to the flower!"};
// 	string[] tut_3 = new string[]{"I wouldn't want to end in that hole over there!" };
// 	string[] tut_6 = new string[]{"Make sure both Pedros are on the right place for me to get to the flower"};
// 	string[] tut_9 = new string[]{"You will only get 3 stars if you do the least amount of moves possible.", "Those dots next to the bar on the right represent the number of best moves"};
// 	string[] tut_12 = new string[]{"That yellow flower just started to grow!","That must mean a new spring perhaps isn't that far away", "Don't put anything over it!, better to preserve them"};
//     string[] tut_41 = new string[]{"Those birds are called Icarus", "They send a strong air current in the tile right in front of them", "If I go near It'll change my direction!"};
//     string[] tut_50 = new string[]{"That's a fragile tile", "It'll fall quickly, but don't worry, I can outrun them"};
// 	string[] tut_52 = new string[]{"Make sure not to linger on one of those fragile tiles or I'll fall", "I can go through them, but not stop on them"};				
//     string[] tut_55 = new string[]{"I'm pretty sure Icarus can blow air faster than that fragile tile can fall"};
//     string[] tut_68 = new string[]{"I don't always need the wind to get to the flower!"};
//     string[] tut_81 = new string[]{"That bag with the circle becomes Pedro once I go over it!"};
//     string[] tut_121 = new string[]{"That bag with the arrow has an Icarus sleeping inside!", "I'll wake it once I go over it."};
//     string[] tut_122 = new string[]{"You can tell by the arrow in the bag, the direction it'll blow when it wakes!"};
// 	string[] tut_160 = new string[]{"This is the end of our adventure for now", "Be sure to come back soon for more levels and new friends!"};
	
//     //Choose one of the dialogs to happen after 30s of innactivity

//     string[] random1 = new string[] {"Focus! You can do it!"};
//     string[] random2 = new string[] {"If you have trouble arranging the pieces, press that button with a bulb over there!"};
//     string[] random3 = new string[] {"If you're stuck, Restart!"};
//     //Choose one of the dialogs to happen after 5 consecutive deaths
//     string[] random4 = new string[] {"You can restart the level and ask for a hint once I'm back in my starting position"};
//     string[] random5 = new string[] {"We almost made it! Keep trying!"};
//     string[] random6 = new string[] {"This time we'll make it, I'm sure!"};


// 	string[] music = new string[] {"Music"};
// 	string[] sfx = new string[] {"SFX"};

// //Spanish Assets SP

// 	string[] tut_button_sp = new string[]{"??Acomoda las criaturas y ay??dame a llegar a la flor roja!", "Una vez que me muevo, ??no podr??s cambiar las piezas de lugar!", "Si no logras resolver el nivel, pide una pista despu??s de reiniciarlo"};
//     	string[] tut_1_sp = new string[]{"??Hola!, mi nombre es Zenko", "Ay??dame a llegar a la flor roja deslizando tu dedo por la pantalla"};
// 	string[] tut_2_sp = new string[]{"Esa pieza se llama Pedro", "Puedes moverla por el tablero para poder utilizarlo como barrera", "??Acom??dalo antes de moverme hacia la flor roja!"};
// 	string[] tut_3_sp = new string[]{"??No me gustar??a terminar en ese agujero!"};
// 	string[] tut_6_sp = new string[]{"Aseg??rate de que ambos Pedro est??n en el lugar correcto ANTES de moverme hacia la flor"};
// 	string[] tut_9_sp = new string[]{"Solo obtendr??s las 3 estrellas si solucionas el tablero en la menor cantidad de movimientos posibles.", "Los puntos al lado de la barra representan la menor cantidad de movimientos posibles"};
// 	string[] tut_12_sp = new string[]{"??Esa peque??a flor amarilla est?? creciendo!", "Eso significa que la primavera est?? cerca", "??No pongas nada encima, hay que conservarlas!"};
//     	string[] tut_41_sp = new string[]{"Esos p??jaros se llaman Icarus", "Env??an una fuerte corriente de aire frente a ellos", "Si paso frente a ellos, ??cambiar??n mi direcci??n!"};
//     	string[] tut_50_sp = new string[]{"Eso es piso fr??gil", "Se destruye r??pidamente... pero yo soy m??s r??pido"};
// 	string[] tut_52_sp = new string[]{"Recuerda no detenerme sobre el piso fr??gil, o me caer??", "Puedo pasar sobre ellas, pero no soportan mi peso al detenerme"};				
//     	string[] tut_55_sp = new string[]{"Tengo la certeza de que el aire de Icarus es m??s r??pido que el suelo fr??gil"};
//     	string[] tut_68_sp = new string[]{"??No siempre necesito aire para llegar a la flor!"};
//     	string[] tut_81_sp = new string[]{"??Esa bolsa con el c??rculo se transforma en Pedro al pasar sobre ella!"};
//     	string[] tut_121_sp = new string[]{"??Dentro de esa bolsa con la flecha est?? Icarus dormido!, "Lo despertar?? al pasar encima"};
//     	string[] tut_122_sp = new string[]{"La direcci??n de la flecha en la bolsa, ??es la misma direcci??n en la Icarus soplar?? al despertar!"};
// 	string[] tut_160_sp = new string[]{"Aqu?? termina nuestra aventura por ahora", "??Recuerda regresar pronto para jugar en nuevos niveles y conocer nuevos amigos!"};
	
//     //Choose one of the dialogs to happen after 30s of innactivity

//     string[] random1_sp = new string[] {"??Conc??ntrate!??T?? puedes!"};
//     string[] random2_sp = new string[] {"Si llegas a tener problemas acomodando las piezas, ??presiona el bot??n con el foco!"};
//     string[] random3_sp = new string[] {"Cuando no encuentres la respuesta, ??vuelve a empezar!"};
//     //Choose one of the dialogs to happen after 5 consecutive deaths
//     string[] random4_sp = new string[] {"Puedes reiniciar el nivel y pedir una pista en cuanto regrese a mi posici??n inicial"};
//     string[] random5_sp = new string[] {"??Estuvimos a punto de lograrlo! ??Sigue intentando!"};
//     string[] random6_sp = new string[] {"??Esta vez s?? lo vamos a lograr!"};


// 	string[] music_sp = new string[] {"M??sica"};
// 	string[] sfx_sp = new string[] {"Efectos de sonido"};
// 	//yo lo dejaria como sfx
// 	//string[] sfx_sp = new string[] {"SFX"};


// 	void Awake(){
// 		Instance = this;
// 		if (!initialized)
// 		PopulateDictionary();
// 		initialized = true;
// 	}

// 	public void PopulateDictionary(){
// 		tutDic.Add(0,tut_button);
// 		tutDic.Add(1,tut_1);
// 		tutDic.Add(2,tut_2);
// 		tutDic.Add(3,tut_3);
// 		tutDic.Add(6,tut_6);
// 		tutDic.Add(9,tut_9);
// 		tutDic.Add(12,tut_12);
// 		tutDic.Add(41,tut_41);
// 		tutDic.Add(50,tut_50);
// 		tutDic.Add(52,tut_52);
// 		tutDic.Add(55,tut_55);
// 		tutDic.Add(68,tut_68);
// 		tutDic.Add(81,tut_81);
// 		tutDic.Add(121,tut_121);
// 		tutDic.Add(122,tut_122);
// 		tutDic.Add(160,tut_160);
// 	}

// }
