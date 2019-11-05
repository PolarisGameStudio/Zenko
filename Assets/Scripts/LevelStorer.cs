using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;

public class LevelStats {
	public int levelnum;
	public int turns; //best turn solution
	public bool islocked;
	public int rating;

	public LevelStats (int newlevelnum, int newturns, bool newislocked, int newrating){
		levelnum = newlevelnum;
		turns = newturns;
		islocked = newislocked;
		rating = newrating;
	}
}

public class Tile{
	public GameObject tileObj;
	public string type;
	public bool isTaken;
	public string seedType;
	public string isSideways;
	public Tile(GameObject obj, string t, bool it, string seed){
		tileObj = obj;
		type = t;
		isTaken = it;
		seedType = seed;
		isSideways = null;
	}
	public Tile(GameObject obj, string t, bool it){
		tileObj = obj;
		type = t;
		isTaken = it;
		seedType = null;
		isSideways = null;
	}
}


public class LevelStorer : MonoBehaviour {
	public static int efficientturns;
	public static Dictionary<int, LevelStats> leveldic = new Dictionary<int, LevelStats>();
	static public int maxlevels;
	bool hasinitd;
	private static LevelStorer instance = null;
	int reset;
	StreamWriter normalmaps;
	public static string importantValues;

	public static void PopulateImportantValues(){

	}

	public static void UpdateImportantValues(int place, int value){

	}

	public static void PopulateLevelDictionary(){

		LevelStats lv1  = new LevelStats(1,2,false,0);
		LevelStats lv2  = new LevelStats(2,3,true,0); 
		LevelStats lv3  = new LevelStats(3,3,true,0); 
		LevelStats lv4  = new LevelStats(4,4,true,0); 
		LevelStats lv5  = new LevelStats(5,2,true,0); 
		LevelStats lv6  = new LevelStats(6,4,true,0); 
		LevelStats lv7  = new LevelStats(7,4,true,0); 
		LevelStats lv8  = new LevelStats(8,3,true,0); 
		LevelStats lv9  = new LevelStats(9,4,true,0); 
		LevelStats lv10 = new LevelStats(10,3,true,0); 
		LevelStats lv11 = new LevelStats(11,5,true,0); //too hard, need to make an easier tile leveldic
		LevelStats lv12 = new LevelStats(12,5,true,0); 
		LevelStats lv13 = new LevelStats(13,4,true,0); 
		LevelStats lv14 = new LevelStats(14,4,true,0); 
		LevelStats lv15 = new LevelStats(15,3,true,0); 
		LevelStats lv16 = new LevelStats(16,4,true,0); 
		LevelStats lv17 = new LevelStats(17,5,true,0); 
		LevelStats lv18 = new LevelStats(18,5,true,0); 
		LevelStats lv19 = new LevelStats(19,4,true,0); 
		LevelStats lv20 = new LevelStats(20,5,true,0); 
		LevelStats lv21 = new LevelStats(21,5,true,0); 
		LevelStats lv22 = new LevelStats(22,4,true,0); 
		LevelStats lv23 = new LevelStats(23,6,true,0); 
		LevelStats lv24 = new LevelStats(24,5,true,0); 
		LevelStats lv25 = new LevelStats(25,6,true,0); 
		LevelStats lv26 = new LevelStats(26,7,true,0); 
		LevelStats lv27 = new LevelStats(27,7,true,0); 
		LevelStats lv28 = new LevelStats(28,4,true,0); 
		LevelStats lv29 = new LevelStats(29,8,true,0); 
		LevelStats lv30 = new LevelStats(30,5,true,0); 
		LevelStats lv31 = new LevelStats(31,5,true,0); 
		LevelStats lv32 = new LevelStats(32,3,true,0); 
		LevelStats lv33 = new LevelStats(33,1,true,0); 
		LevelStats lv34 = new LevelStats(34,3,true,0); 
		LevelStats lv35 = new LevelStats(35,4,true,0); 
		LevelStats lv36 = new LevelStats(36,2,true,0); 
		LevelStats lv37 = new LevelStats(37,5,true,0); 
		LevelStats lv38 = new LevelStats(38,5,true,0); 
		LevelStats lv39 = new LevelStats(39,7,true,0); 
		LevelStats lv40 = new LevelStats(40,5,true,0); 
		LevelStats lv41 = new LevelStats(41,3,true,0); 
		LevelStats lv42 = new LevelStats(42,4,true,0); 
		LevelStats lv43 = new LevelStats(43,5,true,0); 
		LevelStats lv44 = new LevelStats(44,6,true,0); 
		LevelStats lv45 = new LevelStats(45,4,true,0); 
		LevelStats lv46 = new LevelStats(46,3,true,0); 
		LevelStats lv47 = new LevelStats(47,3,true,0); 
		LevelStats lv48 = new LevelStats(48,3,true,0); 
		LevelStats lv49 = new LevelStats(49,4,true,0); 
		LevelStats lv50 = new LevelStats(50,7,true,0); 
		LevelStats lv51 = new LevelStats(51,5,true,0); 
		LevelStats lv52 = new LevelStats(52,4,true,0); 
		LevelStats lv53 = new LevelStats(53,7,true,0); 
		LevelStats lv54 = new LevelStats(54,5,true,0); 
		LevelStats lv55 = new LevelStats(55,4,true,0);  //save 1
		LevelStats lv56 = new LevelStats(56,6,true,0); 
		LevelStats lv57 = new LevelStats(57,7,true,0); 
		LevelStats lv58 = new LevelStats(58,7,true,0); 
		LevelStats lv59 = new LevelStats(59,9,true,0); 
		LevelStats lv60 = new LevelStats(60,9,true,0); //swap with 62
		LevelStats lv61 = new LevelStats(61,8,true,0); 
		LevelStats lv62 = new LevelStats(62,9,true,0); 
		LevelStats lv63 = new LevelStats(63,8,true,0); 
		LevelStats lv64 = new LevelStats(64,7,true,0); 
		LevelStats lv65 = new LevelStats(65,7,true,0); 
		LevelStats lv66 = new LevelStats(66,7,true,0); 
		LevelStats lv67 = new LevelStats(67,1,true,0); 
		LevelStats lv68 = new LevelStats(68,7,true,0); 
		LevelStats lv69 = new LevelStats(69,4,true,0); 
		LevelStats lv70 = new LevelStats(70,4,true,0); 
		LevelStats lv71 = new LevelStats(71,6,true,0); 
		LevelStats lv72 = new LevelStats(72,6,true,0); 
		LevelStats lv73 = new LevelStats(73,5,true,0); 
		LevelStats lv74 = new LevelStats(74,6,true,0); 
		LevelStats lv75 = new LevelStats(75,3,true,0); 
		LevelStats lv76 = new LevelStats(76,7,true,0); 
		LevelStats lv77 = new LevelStats(77,7,true,0); 
		LevelStats lv78 = new LevelStats(78,2,true,0); 
		LevelStats lv79 = new LevelStats(79,7,true,0); 
		LevelStats lv80 = new LevelStats(80,5,true,0); 
		LevelStats lv81 = new LevelStats(81,6,true,0); 
		LevelStats lv82 = new LevelStats(82,5,true,0); 
		LevelStats lv83 = new LevelStats(83,5,true,0); 
		LevelStats lv84 = new LevelStats(84,6,true,0); 
		LevelStats lv85 = new LevelStats(85,4,true,0); 
		LevelStats lv86 = new LevelStats(86,4,true,0); //extra
		LevelStats lv87 = new LevelStats(87,4,true,0); 
		LevelStats lv88 = new LevelStats(88,7,true,0); 
		LevelStats lv89 = new LevelStats(89,3,true,0); 
		LevelStats lv90 = new LevelStats(90,4,true,0); 
		LevelStats lv91 = new LevelStats(91,1,true,0); 
		LevelStats lv92 = new LevelStats(92,7,true,0); 
		LevelStats lv93 = new LevelStats(93,6,true,0); 
		LevelStats lv94 = new LevelStats(94,6,true,0); 
		LevelStats lv95 = new LevelStats(95,7,true,0); 
		LevelStats lv96 = new LevelStats(96,7,true,0); 
		LevelStats lv97 = new LevelStats(97,10,true,0); 
		LevelStats lv98 = new LevelStats(98,6,true,0); 
		LevelStats lv99 = new LevelStats(99,3,true,0); 
		LevelStats lv100= new LevelStats(100,7,true,0); 
		LevelStats lv101  = new LevelStats(101,2,true,0);
		LevelStats lv102  = new LevelStats(102,3,true,0); 
		LevelStats lv103  = new LevelStats(103,3,true,0); 
		LevelStats lv104  = new LevelStats(104,4,true,0); 
		LevelStats lv105  = new LevelStats(105,2,true,0); 
		LevelStats lv106  = new LevelStats(106,4,true,0); 
		LevelStats lv107  = new LevelStats(107,4,true,0); 
		LevelStats lv108  = new LevelStats(108,3,true,0); 
		LevelStats lv109  = new LevelStats(109,4,true,0); 
		LevelStats lv110 = new LevelStats(110,3,true,0); 
		LevelStats lv111 = new LevelStats(111,5,true,0); //too hard, need to make an easier tile leveldic
		LevelStats lv112 = new LevelStats(112,5,true,0); 
		LevelStats lv113 = new LevelStats(113,4,true,0); 
		LevelStats lv114 = new LevelStats(114,4,true,0); 
		LevelStats lv115 = new LevelStats(115,3,true,0); 
		LevelStats lv116 = new LevelStats(116,4,true,0); 
		LevelStats lv117 = new LevelStats(117,5,true,0); 
		LevelStats lv118 = new LevelStats(118,5,true,0); 
		LevelStats lv119 = new LevelStats(119,4,true,0); 
		LevelStats lv120 = new LevelStats(120,5,true,0); 
		LevelStats lv121 = new LevelStats(121,5,true,0); 
		LevelStats lv122 = new LevelStats(122,4,true,0); 
		LevelStats lv123 = new LevelStats(123,6,true,0); 
		LevelStats lv124 = new LevelStats(124,5,true,0); 
		LevelStats lv125 = new LevelStats(125,6,true,0); 
		LevelStats lv126 = new LevelStats(126,7,true,0); 
		LevelStats lv127 = new LevelStats(127,7,true,0); 
		LevelStats lv128 = new LevelStats(128,4,true,0); 
		LevelStats lv129 = new LevelStats(129,8,true,0); 
		LevelStats lv130 = new LevelStats(130,5,true,0); 
		LevelStats lv131 = new LevelStats(131,5,true,0); 
		LevelStats lv132 = new LevelStats(132,3,true,0); 
		LevelStats lv133 = new LevelStats(133,1,true,0); 
		LevelStats lv134 = new LevelStats(134,3,true,0); 
		LevelStats lv135 = new LevelStats(135,4,true,0); 
		LevelStats lv136 = new LevelStats(136,2,true,0); 
		LevelStats lv137 = new LevelStats(137,5,true,0); 
		LevelStats lv138 = new LevelStats(138,5,true,0); 
		LevelStats lv139 = new LevelStats(139,7,true,0); 
		LevelStats lv140 = new LevelStats(140,5,true,0); 
		LevelStats lv141 = new LevelStats(141,3,true,0); 
		LevelStats lv142 = new LevelStats(142,4,true,0); 
		LevelStats lv143 = new LevelStats(143,5,true,0); 
		LevelStats lv144 = new LevelStats(144,6,true,0); 
		LevelStats lv145 = new LevelStats(145,4,true,0); 
		LevelStats lv146 = new LevelStats(146,3,true,0); 
		LevelStats lv147 = new LevelStats(147,3,true,0); 
		LevelStats lv148 = new LevelStats(148,3,true,0); 
		LevelStats lv149 = new LevelStats(149,4,true,0); 
		LevelStats lv150 = new LevelStats(150,7,true,0); 
		LevelStats lv151 = new LevelStats(151,5,true,0); 
		LevelStats lv152 = new LevelStats(152,4,true,0); 
		LevelStats lv153 = new LevelStats(153,7,true,0); 
		LevelStats lv154 = new LevelStats(154,5,true,0); 
		LevelStats lv155 = new LevelStats(155,4,true,0);  //save 1
		LevelStats lv156 = new LevelStats(156,6,true,0); 
		LevelStats lv157 = new LevelStats(157,7,true,0); 
		LevelStats lv158 = new LevelStats(158,7,true,0); 
		LevelStats lv159 = new LevelStats(159,9,true,0); 
		LevelStats lv160 = new LevelStats(160,9,true,0); //swap with 62
		LevelStats lv161 = new LevelStats(161,8,true,0); 
		LevelStats lv162 = new LevelStats(162,9,true,0); 
		LevelStats lv163 = new LevelStats(163,8,true,0); 
		LevelStats lv164 = new LevelStats(164,7,true,0); 
		LevelStats lv165 = new LevelStats(165,7,true,0); 
		LevelStats lv166 = new LevelStats(166,7,true,0); 
		LevelStats lv167 = new LevelStats(167,1,true,0); 
		LevelStats lv168 = new LevelStats(168,7,true,0); 
		LevelStats lv169 = new LevelStats(169,4,true,0); 
		LevelStats lv170 = new LevelStats(170,4,true,0); 
		LevelStats lv171 = new LevelStats(171,6,true,0); 
		LevelStats lv172 = new LevelStats(172,6,true,0); 
		LevelStats lv173 = new LevelStats(173,5,true,0); 
		LevelStats lv174 = new LevelStats(174,6,true,0); 
		LevelStats lv175 = new LevelStats(175,3,true,0); 
		LevelStats lv176 = new LevelStats(176,7,true,0); 
		LevelStats lv177 = new LevelStats(177,7,true,0); 
		LevelStats lv178 = new LevelStats(178,2,true,0); 
		LevelStats lv179 = new LevelStats(179,7,true,0); 
		LevelStats lv180 = new LevelStats(190,5,true,0); 
		LevelStats lv181 = new LevelStats(181,6,true,0); 
		LevelStats lv182 = new LevelStats(182,5,true,0); 
		LevelStats lv183 = new LevelStats(183,5,true,0); 
		LevelStats lv184 = new LevelStats(184,6,true,0); 
		LevelStats lv185 = new LevelStats(185,4,true,0); 
		LevelStats lv186 = new LevelStats(186,4,true,0); //extra
		LevelStats lv187 = new LevelStats(187,4,true,0); 
		LevelStats lv188 = new LevelStats(188,7,true,0); 
		LevelStats lv189 = new LevelStats(189,3,true,0); 
		LevelStats lv190 = new LevelStats(190,4,true,0); 
		LevelStats lv191 = new LevelStats(191,1,true,0); 
		LevelStats lv192 = new LevelStats(192,7,true,0); 
		LevelStats lv193 = new LevelStats(193,6,true,0); 
		LevelStats lv194 = new LevelStats(194,6,true,0); 
		LevelStats lv195 = new LevelStats(195,7,true,0); 
		LevelStats lv196 = new LevelStats(196,7,true,0); 
		LevelStats lv197 = new LevelStats(197,10,true,0); 
		LevelStats lv198 = new LevelStats(198,6,true,0); 
		LevelStats lv199 = new LevelStats(199,3,true,0); 
		LevelStats lv200= new LevelStats(200,7,true,0); 

		leveldic.Add (1, lv1);
		leveldic.Add (2, lv2);
		leveldic.Add (3, lv3);
		leveldic.Add (4, lv4);
		leveldic.Add (5, lv5);
		leveldic.Add (6, lv6);
		leveldic.Add (7, lv7);
		leveldic.Add (8, lv8);
		leveldic.Add (9, lv9);
		leveldic.Add (10, lv10);
		leveldic.Add (11, lv11);
		leveldic.Add (12, lv12);
		leveldic.Add (13, lv13);
		leveldic.Add (14, lv14);
		leveldic.Add (15, lv15);
		leveldic.Add (16, lv16);
		leveldic.Add (17, lv17);
		leveldic.Add (18, lv18);
		leveldic.Add (19, lv19);
		leveldic.Add (20, lv20);
		leveldic.Add (21, lv21);
		leveldic.Add (22, lv22);
		leveldic.Add (23, lv23);
		leveldic.Add (24, lv24);
		leveldic.Add (25, lv25);
		leveldic.Add (26, lv26);
		leveldic.Add (27, lv27);
		leveldic.Add (28, lv28);
		leveldic.Add (29, lv29);
		leveldic.Add (30, lv30);
		leveldic.Add (31, lv31);
		leveldic.Add (32, lv32);
		leveldic.Add (33, lv33);
		leveldic.Add (34, lv34);
		leveldic.Add (35, lv35);
		leveldic.Add (36, lv36);
		leveldic.Add (37, lv37);
		leveldic.Add (38, lv38);
		leveldic.Add (39, lv39);
		leveldic.Add (40, lv40);
		leveldic.Add (41, lv41);
		leveldic.Add (42, lv42);
		leveldic.Add (43, lv43);
		leveldic.Add (44, lv44);
		leveldic.Add (45, lv45);
		leveldic.Add (46, lv46);
		leveldic.Add (47, lv47);
		leveldic.Add (48, lv48);
		leveldic.Add (49, lv49);
		leveldic.Add (50, lv50);
		leveldic.Add (51, lv51);
		leveldic.Add (52, lv52);
		leveldic.Add (53, lv53);
		leveldic.Add (54, lv54);
		leveldic.Add (55, lv55);
		leveldic.Add (56, lv56);
		leveldic.Add (57, lv57);
		leveldic.Add (58, lv58);
		leveldic.Add (59, lv59);
		leveldic.Add (60, lv60);
		leveldic.Add (61, lv61);
		leveldic.Add (62, lv62);
		leveldic.Add (63, lv63);
		leveldic.Add (64, lv64);
		leveldic.Add (65, lv65);
		leveldic.Add (66, lv66);
		leveldic.Add (67, lv67);
		leveldic.Add (68, lv68);
		leveldic.Add (69, lv69);
		leveldic.Add (70, lv70);
		leveldic.Add (71, lv71);
		leveldic.Add (72, lv72);
		leveldic.Add (73, lv73);
		leveldic.Add (74, lv74);
		leveldic.Add (75, lv75);
		leveldic.Add (76, lv76);
		leveldic.Add (77, lv77);
		leveldic.Add (78, lv78);
		leveldic.Add (79, lv79);
		leveldic.Add (80, lv80);
		leveldic.Add (81, lv81);
		leveldic.Add (82, lv82);
		leveldic.Add (83, lv83);
		leveldic.Add (84, lv84);
		leveldic.Add (85, lv85);
		leveldic.Add (86, lv86);
		leveldic.Add (87, lv87);
		leveldic.Add (88, lv88);
		leveldic.Add (89, lv89);
		leveldic.Add (90, lv90);
		leveldic.Add (91, lv91);
		leveldic.Add (92, lv92);
		leveldic.Add (93, lv93);
		leveldic.Add (94, lv94);
		leveldic.Add (95, lv95);
		leveldic.Add (96, lv96);
		leveldic.Add (97, lv97);
		leveldic.Add (98, lv98);
		leveldic.Add (99, lv99);
		leveldic.Add (100, lv100);
		leveldic.Add (101, lv101);
		leveldic.Add (102, lv102);
		leveldic.Add (103, lv103);
		leveldic.Add (104, lv104);
		leveldic.Add (105, lv105);
		leveldic.Add (106, lv106);
		leveldic.Add (107, lv107);
		leveldic.Add (108, lv108);
		leveldic.Add (109, lv109);
		leveldic.Add (110, lv110);
		leveldic.Add (111, lv111);
		leveldic.Add (112, lv112);
		leveldic.Add (113, lv113);
		leveldic.Add (114, lv114);
		leveldic.Add (115, lv115);
		leveldic.Add (116, lv116);
		leveldic.Add (117, lv117);
		leveldic.Add (118, lv118);
		leveldic.Add (119, lv119);
		leveldic.Add (120, lv120);
		leveldic.Add (121, lv121);
		leveldic.Add (122, lv122);
		leveldic.Add (123, lv123);
		leveldic.Add (124, lv124);
		leveldic.Add (125, lv125);
		leveldic.Add (126, lv126);
		leveldic.Add (127, lv127);
		leveldic.Add (128, lv128);
		leveldic.Add (129, lv129);
		leveldic.Add (130, lv130);
		leveldic.Add (131, lv131);
		leveldic.Add (132, lv132);
		leveldic.Add (133, lv133);
		leveldic.Add (134, lv134);
		leveldic.Add (135, lv135);
		leveldic.Add (136, lv136);
		leveldic.Add (137, lv137);
		leveldic.Add (138, lv138);
		leveldic.Add (139, lv139);
		leveldic.Add (140, lv140);
		leveldic.Add (141, lv141);
		leveldic.Add (142, lv142);
		leveldic.Add (143, lv143);
		leveldic.Add (144, lv144);
		leveldic.Add (145, lv145);
		leveldic.Add (146, lv146);
		leveldic.Add (147, lv147);
		leveldic.Add (148, lv148);
		leveldic.Add (149, lv149);
		leveldic.Add (150, lv150);
		leveldic.Add (151, lv151);
		leveldic.Add (152, lv152);
		leveldic.Add (153, lv153);
		leveldic.Add (154, lv154);
		leveldic.Add (155, lv155);
		leveldic.Add (156, lv156);
		leveldic.Add (157, lv157);
		leveldic.Add (158, lv158);
		leveldic.Add (159, lv159);
		leveldic.Add (160, lv160);
		leveldic.Add (161, lv161);
		leveldic.Add (162, lv162);
		leveldic.Add (163, lv163);
		leveldic.Add (164, lv164);
		leveldic.Add (165, lv165);
		leveldic.Add (166, lv166);
		leveldic.Add (167, lv167);
		leveldic.Add (168, lv168);
		leveldic.Add (169, lv169);
		leveldic.Add (170, lv170);
		leveldic.Add (171, lv171);
		leveldic.Add (172, lv172);
		leveldic.Add (173, lv173);
		leveldic.Add (174, lv174);
		leveldic.Add (175, lv175);
		leveldic.Add (176, lv176);
		leveldic.Add (177, lv177);
		leveldic.Add (178, lv178);
		leveldic.Add (179, lv179);
		leveldic.Add (180, lv180);
		leveldic.Add (181, lv181);
		leveldic.Add (182, lv182);
		leveldic.Add (183, lv183);
		leveldic.Add (184, lv184);
		leveldic.Add (185, lv185);
		leveldic.Add (186, lv186);
		leveldic.Add (187, lv187);
		leveldic.Add (188, lv188);
		leveldic.Add (189, lv189);
		leveldic.Add (190, lv190);
		leveldic.Add (191, lv191);
		leveldic.Add (192, lv192);
		leveldic.Add (193, lv193);
		leveldic.Add (194, lv194);
		leveldic.Add (195, lv195);
		leveldic.Add (196, lv196);
		leveldic.Add (197, lv197);
		leveldic.Add (198, lv198);
		leveldic.Add (199, lv199);
		leveldic.Add (200, lv200);
	}


	// Use this for initialization
	void Awake () {
//		Debug.Log(instance);
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			//return;
		}
		else{
			Destroy(this.gameObject);
			Debug.Log("Destroyed LevelStorer");
			return;
		}
		//PlayerPrefs.DeleteAll();
/*		reset = PlayerPrefs.GetInt ("Reset");
		if (reset == 0) {
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt ("Reset", 1);
		}*/
		/*if (PlayerPrefs.HasKey ("Loaded")) {
			Debug.Log ("Has playerpref");
		} else {*/
			//Load levels for the first time. Init values. only level 1 unlocked.
		//Debug.Log ("Giving loaded");
		

		//PlayerPrefs.SetInt ("Loaded", 1);

		//PlayerPrefs.DeleteAll();
		//GameManager.mycurrency = PlayerPrefs.GetInt("Currency");
		//GameObject.Find("CurrencyHolder").GetComponentInChildren<Text>().text = GameManager.mycurrency.ToString();
	}
		//PlayerPrefs.DeleteAll();
	//}
	/*void Update () {
		if (efficientturns <= 0) {
			 (LevelManager.levelnum);
		}
	}*/


	void OnApplicationQuit(){
		//normalmaps.Close();
	}

	public static void PopulatePlayerPrefs(){ //populates ratings
		for(int i=1; i<leveldic.Count+1; i++){
			string mystring = "Level"+i+"Rating";
			//Debug.Log(PlayerPrefs.GetInt(mystring));
			if(PlayerPrefs.GetInt(mystring)>0){
				//leveldic[i].islocked = false;
				//leveldic[i+1].islocked = false;
				PlayerPrefs.SetInt(mystring, 0);
			}


		}		
	}

	public static void PopulateCloudData(){

	}

	public static void AddRatingsToDictionary(){
		for(int i=1; i<leveldic.Count+1; i++){
			string mystring = "Level"+i+"Rating";

			if(PlayerPrefs.GetInt(mystring)>0){
				leveldic[i].islocked = false;
				leveldic[i+1].islocked = false;
				leveldic[i].rating = PlayerPrefs.GetInt(mystring);
			}
		}
	}

	public static void Lookfor(int levelnum){ //unlocks and locks according to save file.
		maxlevels = leveldic.Count ;
//		Debug.Log(leveldic.Count);
	//	Debug.Log ("maxcount" + ma8levels);
		if (levelnum <= maxlevels && levelnum >= 0) {
			efficientturns = leveldic [levelnum].turns;
		//	Debug.Log ("The number of turns for level " + levelnum + " is " + efficientturns);
		} 
		//else if(levelnum < 0){
			//efficientturns
		//}
		else {
		Debug.Log ("No turn number stored" + levelnum + efficientturns);

		}
	}
	public static void UnlockLevel(int levelnum){
		/*int x = leveldic [levelnum].levelnum;
		int y = leveldic [levelnum].turns;
		bool z = false;
		int r = 0; //RatingBehaviour.rating;
		LevelStats newvalue = new LevelStats(x,y,z,r); 
		leveldic [x] = newvalue;*/
		leveldic[levelnum].islocked = false;
	}

	public static void LockLevel(int levelnum){
		int x = leveldic [levelnum].levelnum;
		int y = leveldic [levelnum].turns;
		bool z = true;
		int r = 1; //RatingBehaviour.rating;
		LevelStats newvalue = new LevelStats(x,y,z,r); 
		leveldic [x] = newvalue;
		string mystring = "Level"+levelnum+"Rating";
		PlayerPrefs.SetInt (mystring, 0);
		Debug.Log (levelnum);

	}

	public static void UnlockAllLevels(){
		for (int i = 1; i < leveldic.Count+1 ; i++){
			UnlockLevel (i);
		}
	}

	public static void LockAllLevels(){
		string mystring = "Level1Rating";
		PlayerPrefs.SetInt (mystring, 0);
		for (int i = 2; i < leveldic.Count ; i++){
			LockLevel (i);
		}
	}
}
