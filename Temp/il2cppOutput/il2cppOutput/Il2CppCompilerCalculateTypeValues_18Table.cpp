#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <cstring>
#include <string.h>
#include <stdio.h>
#include <cmath>
#include <limits>
#include <assert.h>
#include <stdint.h>

#include "class-internals.h"
#include "codegen/il2cpp-codegen.h"
#include "object-internals.h"

// System.Void
struct Void_t1841601450;
// System.Char[]
struct CharU5BU5D_t1328083999;
// UnityEngine.Camera
struct Camera_t189460977;
// UnityEngine.GameObject
struct GameObject_t1756533147;
// System.String
struct String_t;
// System.Action`1<Swiping/SwipeDirection>
struct Action_1_t2810927175;




#ifndef RUNTIMEOBJECT_H
#define RUNTIMEOBJECT_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Object

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // RUNTIMEOBJECT_H
#ifndef VALUETYPE_T3507792607_H
#define VALUETYPE_T3507792607_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.ValueType
struct  ValueType_t3507792607  : public RuntimeObject
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t3507792607_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t3507792607_marshaled_com
{
};
#endif // VALUETYPE_T3507792607_H
#ifndef INTPTR_T_H
#define INTPTR_T_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.IntPtr
struct  IntPtr_t 
{
public:
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(IntPtr_t, ___m_value_0)); }
	inline void* get_m_value_0() const { return ___m_value_0; }
	inline void** get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(void* value)
	{
		___m_value_0 = value;
	}
};

struct IntPtr_t_StaticFields
{
public:
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;

public:
	inline static int32_t get_offset_of_Zero_1() { return static_cast<int32_t>(offsetof(IntPtr_t_StaticFields, ___Zero_1)); }
	inline intptr_t get_Zero_1() const { return ___Zero_1; }
	inline intptr_t* get_address_of_Zero_1() { return &___Zero_1; }
	inline void set_Zero_1(intptr_t value)
	{
		___Zero_1 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // INTPTR_T_H
#ifndef ENUM_T2459695545_H
#define ENUM_T2459695545_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Enum
struct  Enum_t2459695545  : public ValueType_t3507792607
{
public:

public:
};

struct Enum_t2459695545_StaticFields
{
public:
	// System.Char[] System.Enum::split_char
	CharU5BU5D_t1328083999* ___split_char_0;

public:
	inline static int32_t get_offset_of_split_char_0() { return static_cast<int32_t>(offsetof(Enum_t2459695545_StaticFields, ___split_char_0)); }
	inline CharU5BU5D_t1328083999* get_split_char_0() const { return ___split_char_0; }
	inline CharU5BU5D_t1328083999** get_address_of_split_char_0() { return &___split_char_0; }
	inline void set_split_char_0(CharU5BU5D_t1328083999* value)
	{
		___split_char_0 = value;
		Il2CppCodeGenWriteBarrier((&___split_char_0), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of System.Enum
struct Enum_t2459695545_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Enum
struct Enum_t2459695545_marshaled_com
{
};
#endif // ENUM_T2459695545_H
#ifndef VECTOR2_T2243707579_H
#define VECTOR2_T2243707579_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Vector2
struct  Vector2_t2243707579 
{
public:
	// System.Single UnityEngine.Vector2::x
	float ___x_0;
	// System.Single UnityEngine.Vector2::y
	float ___y_1;

public:
	inline static int32_t get_offset_of_x_0() { return static_cast<int32_t>(offsetof(Vector2_t2243707579, ___x_0)); }
	inline float get_x_0() const { return ___x_0; }
	inline float* get_address_of_x_0() { return &___x_0; }
	inline void set_x_0(float value)
	{
		___x_0 = value;
	}

	inline static int32_t get_offset_of_y_1() { return static_cast<int32_t>(offsetof(Vector2_t2243707579, ___y_1)); }
	inline float get_y_1() const { return ___y_1; }
	inline float* get_address_of_y_1() { return &___y_1; }
	inline void set_y_1(float value)
	{
		___y_1 = value;
	}
};

struct Vector2_t2243707579_StaticFields
{
public:
	// UnityEngine.Vector2 UnityEngine.Vector2::zeroVector
	Vector2_t2243707579  ___zeroVector_2;
	// UnityEngine.Vector2 UnityEngine.Vector2::oneVector
	Vector2_t2243707579  ___oneVector_3;
	// UnityEngine.Vector2 UnityEngine.Vector2::upVector
	Vector2_t2243707579  ___upVector_4;
	// UnityEngine.Vector2 UnityEngine.Vector2::downVector
	Vector2_t2243707579  ___downVector_5;
	// UnityEngine.Vector2 UnityEngine.Vector2::leftVector
	Vector2_t2243707579  ___leftVector_6;
	// UnityEngine.Vector2 UnityEngine.Vector2::rightVector
	Vector2_t2243707579  ___rightVector_7;
	// UnityEngine.Vector2 UnityEngine.Vector2::positiveInfinityVector
	Vector2_t2243707579  ___positiveInfinityVector_8;
	// UnityEngine.Vector2 UnityEngine.Vector2::negativeInfinityVector
	Vector2_t2243707579  ___negativeInfinityVector_9;

public:
	inline static int32_t get_offset_of_zeroVector_2() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___zeroVector_2)); }
	inline Vector2_t2243707579  get_zeroVector_2() const { return ___zeroVector_2; }
	inline Vector2_t2243707579 * get_address_of_zeroVector_2() { return &___zeroVector_2; }
	inline void set_zeroVector_2(Vector2_t2243707579  value)
	{
		___zeroVector_2 = value;
	}

	inline static int32_t get_offset_of_oneVector_3() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___oneVector_3)); }
	inline Vector2_t2243707579  get_oneVector_3() const { return ___oneVector_3; }
	inline Vector2_t2243707579 * get_address_of_oneVector_3() { return &___oneVector_3; }
	inline void set_oneVector_3(Vector2_t2243707579  value)
	{
		___oneVector_3 = value;
	}

	inline static int32_t get_offset_of_upVector_4() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___upVector_4)); }
	inline Vector2_t2243707579  get_upVector_4() const { return ___upVector_4; }
	inline Vector2_t2243707579 * get_address_of_upVector_4() { return &___upVector_4; }
	inline void set_upVector_4(Vector2_t2243707579  value)
	{
		___upVector_4 = value;
	}

	inline static int32_t get_offset_of_downVector_5() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___downVector_5)); }
	inline Vector2_t2243707579  get_downVector_5() const { return ___downVector_5; }
	inline Vector2_t2243707579 * get_address_of_downVector_5() { return &___downVector_5; }
	inline void set_downVector_5(Vector2_t2243707579  value)
	{
		___downVector_5 = value;
	}

	inline static int32_t get_offset_of_leftVector_6() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___leftVector_6)); }
	inline Vector2_t2243707579  get_leftVector_6() const { return ___leftVector_6; }
	inline Vector2_t2243707579 * get_address_of_leftVector_6() { return &___leftVector_6; }
	inline void set_leftVector_6(Vector2_t2243707579  value)
	{
		___leftVector_6 = value;
	}

	inline static int32_t get_offset_of_rightVector_7() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___rightVector_7)); }
	inline Vector2_t2243707579  get_rightVector_7() const { return ___rightVector_7; }
	inline Vector2_t2243707579 * get_address_of_rightVector_7() { return &___rightVector_7; }
	inline void set_rightVector_7(Vector2_t2243707579  value)
	{
		___rightVector_7 = value;
	}

	inline static int32_t get_offset_of_positiveInfinityVector_8() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___positiveInfinityVector_8)); }
	inline Vector2_t2243707579  get_positiveInfinityVector_8() const { return ___positiveInfinityVector_8; }
	inline Vector2_t2243707579 * get_address_of_positiveInfinityVector_8() { return &___positiveInfinityVector_8; }
	inline void set_positiveInfinityVector_8(Vector2_t2243707579  value)
	{
		___positiveInfinityVector_8 = value;
	}

	inline static int32_t get_offset_of_negativeInfinityVector_9() { return static_cast<int32_t>(offsetof(Vector2_t2243707579_StaticFields, ___negativeInfinityVector_9)); }
	inline Vector2_t2243707579  get_negativeInfinityVector_9() const { return ___negativeInfinityVector_9; }
	inline Vector2_t2243707579 * get_address_of_negativeInfinityVector_9() { return &___negativeInfinityVector_9; }
	inline void set_negativeInfinityVector_9(Vector2_t2243707579  value)
	{
		___negativeInfinityVector_9 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // VECTOR2_T2243707579_H
#ifndef OBJECT_T1021602117_H
#define OBJECT_T1021602117_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Object
struct  Object_t1021602117  : public RuntimeObject
{
public:
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;

public:
	inline static int32_t get_offset_of_m_CachedPtr_0() { return static_cast<int32_t>(offsetof(Object_t1021602117, ___m_CachedPtr_0)); }
	inline intptr_t get_m_CachedPtr_0() const { return ___m_CachedPtr_0; }
	inline intptr_t* get_address_of_m_CachedPtr_0() { return &___m_CachedPtr_0; }
	inline void set_m_CachedPtr_0(intptr_t value)
	{
		___m_CachedPtr_0 = value;
	}
};

struct Object_t1021602117_StaticFields
{
public:
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;

public:
	inline static int32_t get_offset_of_OffsetOfInstanceIDInCPlusPlusObject_1() { return static_cast<int32_t>(offsetof(Object_t1021602117_StaticFields, ___OffsetOfInstanceIDInCPlusPlusObject_1)); }
	inline int32_t get_OffsetOfInstanceIDInCPlusPlusObject_1() const { return ___OffsetOfInstanceIDInCPlusPlusObject_1; }
	inline int32_t* get_address_of_OffsetOfInstanceIDInCPlusPlusObject_1() { return &___OffsetOfInstanceIDInCPlusPlusObject_1; }
	inline void set_OffsetOfInstanceIDInCPlusPlusObject_1(int32_t value)
	{
		___OffsetOfInstanceIDInCPlusPlusObject_1 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_t1021602117_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_t1021602117_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};
#endif // OBJECT_T1021602117_H
#ifndef SWIPEDIRECTION_T3009127793_H
#define SWIPEDIRECTION_T3009127793_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Swiping/SwipeDirection
struct  SwipeDirection_t3009127793 
{
public:
	// System.Int32 Swiping/SwipeDirection::value__
	int32_t ___value___1;

public:
	inline static int32_t get_offset_of_value___1() { return static_cast<int32_t>(offsetof(SwipeDirection_t3009127793, ___value___1)); }
	inline int32_t get_value___1() const { return ___value___1; }
	inline int32_t* get_address_of_value___1() { return &___value___1; }
	inline void set_value___1(int32_t value)
	{
		___value___1 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // SWIPEDIRECTION_T3009127793_H
#ifndef COMPONENT_T3819376471_H
#define COMPONENT_T3819376471_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Component
struct  Component_t3819376471  : public Object_t1021602117
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // COMPONENT_T3819376471_H
#ifndef BEHAVIOUR_T955675639_H
#define BEHAVIOUR_T955675639_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Behaviour
struct  Behaviour_t955675639  : public Component_t3819376471
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BEHAVIOUR_T955675639_H
#ifndef MONOBEHAVIOUR_T1158329972_H
#define MONOBEHAVIOUR_T1158329972_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.MonoBehaviour
struct  MonoBehaviour_t1158329972  : public Behaviour_t955675639
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // MONOBEHAVIOUR_T1158329972_H
#ifndef RAYITO_T1865598542_H
#define RAYITO_T1865598542_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Rayito
struct  Rayito_t1865598542  : public MonoBehaviour_t1158329972
{
public:
	// UnityEngine.Camera Rayito::cam
	Camera_t189460977 * ___cam_2;

public:
	inline static int32_t get_offset_of_cam_2() { return static_cast<int32_t>(offsetof(Rayito_t1865598542, ___cam_2)); }
	inline Camera_t189460977 * get_cam_2() const { return ___cam_2; }
	inline Camera_t189460977 ** get_address_of_cam_2() { return &___cam_2; }
	inline void set_cam_2(Camera_t189460977 * value)
	{
		___cam_2 = value;
		Il2CppCodeGenWriteBarrier((&___cam_2), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // RAYITO_T1865598542_H
#ifndef SCENELOADING_T2546480756_H
#define SCENELOADING_T2546480756_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SceneLoading
struct  SceneLoading_t2546480756  : public MonoBehaviour_t1158329972
{
public:
	// System.Int32 SceneLoading::num
	int32_t ___num_2;
	// System.Int32 SceneLoading::testnum
	int32_t ___testnum_5;

public:
	inline static int32_t get_offset_of_num_2() { return static_cast<int32_t>(offsetof(SceneLoading_t2546480756, ___num_2)); }
	inline int32_t get_num_2() const { return ___num_2; }
	inline int32_t* get_address_of_num_2() { return &___num_2; }
	inline void set_num_2(int32_t value)
	{
		___num_2 = value;
	}

	inline static int32_t get_offset_of_testnum_5() { return static_cast<int32_t>(offsetof(SceneLoading_t2546480756, ___testnum_5)); }
	inline int32_t get_testnum_5() const { return ___testnum_5; }
	inline int32_t* get_address_of_testnum_5() { return &___testnum_5; }
	inline void set_testnum_5(int32_t value)
	{
		___testnum_5 = value;
	}
};

struct SceneLoading_t2546480756_StaticFields
{
public:
	// UnityEngine.GameObject SceneLoading::gamewon
	GameObject_t1756533147 * ___gamewon_3;
	// UnityEngine.GameObject SceneLoading::gamelost
	GameObject_t1756533147 * ___gamelost_4;

public:
	inline static int32_t get_offset_of_gamewon_3() { return static_cast<int32_t>(offsetof(SceneLoading_t2546480756_StaticFields, ___gamewon_3)); }
	inline GameObject_t1756533147 * get_gamewon_3() const { return ___gamewon_3; }
	inline GameObject_t1756533147 ** get_address_of_gamewon_3() { return &___gamewon_3; }
	inline void set_gamewon_3(GameObject_t1756533147 * value)
	{
		___gamewon_3 = value;
		Il2CppCodeGenWriteBarrier((&___gamewon_3), value);
	}

	inline static int32_t get_offset_of_gamelost_4() { return static_cast<int32_t>(offsetof(SceneLoading_t2546480756_StaticFields, ___gamelost_4)); }
	inline GameObject_t1756533147 * get_gamelost_4() const { return ___gamelost_4; }
	inline GameObject_t1756533147 ** get_address_of_gamelost_4() { return &___gamelost_4; }
	inline void set_gamelost_4(GameObject_t1756533147 * value)
	{
		___gamelost_4 = value;
		Il2CppCodeGenWriteBarrier((&___gamelost_4), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // SCENELOADING_T2546480756_H
#ifndef SWIPING_T1887146935_H
#define SWIPING_T1887146935_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Swiping
struct  Swiping_t1887146935  : public MonoBehaviour_t1158329972
{
public:
	// System.Boolean Swiping::eventSent
	bool ___eventSent_10;
	// UnityEngine.Vector2 Swiping::lastPosition
	Vector2_t2243707579  ___lastPosition_11;

public:
	inline static int32_t get_offset_of_eventSent_10() { return static_cast<int32_t>(offsetof(Swiping_t1887146935, ___eventSent_10)); }
	inline bool get_eventSent_10() const { return ___eventSent_10; }
	inline bool* get_address_of_eventSent_10() { return &___eventSent_10; }
	inline void set_eventSent_10(bool value)
	{
		___eventSent_10 = value;
	}

	inline static int32_t get_offset_of_lastPosition_11() { return static_cast<int32_t>(offsetof(Swiping_t1887146935, ___lastPosition_11)); }
	inline Vector2_t2243707579  get_lastPosition_11() const { return ___lastPosition_11; }
	inline Vector2_t2243707579 * get_address_of_lastPosition_11() { return &___lastPosition_11; }
	inline void set_lastPosition_11(Vector2_t2243707579  value)
	{
		___lastPosition_11 = value;
	}
};

struct Swiping_t1887146935_StaticFields
{
public:
	// System.String Swiping::mydirection
	String_t* ___mydirection_2;
	// UnityEngine.Vector2 Swiping::firstPressPos
	Vector2_t2243707579  ___firstPressPos_3;
	// UnityEngine.Vector2 Swiping::secondPressPos
	Vector2_t2243707579  ___secondPressPos_4;
	// UnityEngine.Vector2 Swiping::currentSwipe
	Vector2_t2243707579  ___currentSwipe_5;
	// System.Boolean Swiping::canswipe
	bool ___canswipe_6;
	// UnityEngine.GameObject Swiping::The_Dragged
	GameObject_t1756533147 * ___The_Dragged_7;
	// System.Action`1<Swiping/SwipeDirection> Swiping::Swipe
	Action_1_t2810927175 * ___Swipe_8;
	// System.Boolean Swiping::swiping
	bool ___swiping_9;

public:
	inline static int32_t get_offset_of_mydirection_2() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___mydirection_2)); }
	inline String_t* get_mydirection_2() const { return ___mydirection_2; }
	inline String_t** get_address_of_mydirection_2() { return &___mydirection_2; }
	inline void set_mydirection_2(String_t* value)
	{
		___mydirection_2 = value;
		Il2CppCodeGenWriteBarrier((&___mydirection_2), value);
	}

	inline static int32_t get_offset_of_firstPressPos_3() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___firstPressPos_3)); }
	inline Vector2_t2243707579  get_firstPressPos_3() const { return ___firstPressPos_3; }
	inline Vector2_t2243707579 * get_address_of_firstPressPos_3() { return &___firstPressPos_3; }
	inline void set_firstPressPos_3(Vector2_t2243707579  value)
	{
		___firstPressPos_3 = value;
	}

	inline static int32_t get_offset_of_secondPressPos_4() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___secondPressPos_4)); }
	inline Vector2_t2243707579  get_secondPressPos_4() const { return ___secondPressPos_4; }
	inline Vector2_t2243707579 * get_address_of_secondPressPos_4() { return &___secondPressPos_4; }
	inline void set_secondPressPos_4(Vector2_t2243707579  value)
	{
		___secondPressPos_4 = value;
	}

	inline static int32_t get_offset_of_currentSwipe_5() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___currentSwipe_5)); }
	inline Vector2_t2243707579  get_currentSwipe_5() const { return ___currentSwipe_5; }
	inline Vector2_t2243707579 * get_address_of_currentSwipe_5() { return &___currentSwipe_5; }
	inline void set_currentSwipe_5(Vector2_t2243707579  value)
	{
		___currentSwipe_5 = value;
	}

	inline static int32_t get_offset_of_canswipe_6() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___canswipe_6)); }
	inline bool get_canswipe_6() const { return ___canswipe_6; }
	inline bool* get_address_of_canswipe_6() { return &___canswipe_6; }
	inline void set_canswipe_6(bool value)
	{
		___canswipe_6 = value;
	}

	inline static int32_t get_offset_of_The_Dragged_7() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___The_Dragged_7)); }
	inline GameObject_t1756533147 * get_The_Dragged_7() const { return ___The_Dragged_7; }
	inline GameObject_t1756533147 ** get_address_of_The_Dragged_7() { return &___The_Dragged_7; }
	inline void set_The_Dragged_7(GameObject_t1756533147 * value)
	{
		___The_Dragged_7 = value;
		Il2CppCodeGenWriteBarrier((&___The_Dragged_7), value);
	}

	inline static int32_t get_offset_of_Swipe_8() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___Swipe_8)); }
	inline Action_1_t2810927175 * get_Swipe_8() const { return ___Swipe_8; }
	inline Action_1_t2810927175 ** get_address_of_Swipe_8() { return &___Swipe_8; }
	inline void set_Swipe_8(Action_1_t2810927175 * value)
	{
		___Swipe_8 = value;
		Il2CppCodeGenWriteBarrier((&___Swipe_8), value);
	}

	inline static int32_t get_offset_of_swiping_9() { return static_cast<int32_t>(offsetof(Swiping_t1887146935_StaticFields, ___swiping_9)); }
	inline bool get_swiping_9() const { return ___swiping_9; }
	inline bool* get_address_of_swiping_9() { return &___swiping_9; }
	inline void set_swiping_9(bool value)
	{
		___swiping_9 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // SWIPING_T1887146935_H
#ifndef TURNBEHAVIOUR_T179487338_H
#define TURNBEHAVIOUR_T179487338_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// TurnBehaviour
struct  TurnBehaviour_t179487338  : public MonoBehaviour_t1158329972
{
public:

public:
};

struct TurnBehaviour_t179487338_StaticFields
{
public:
	// System.Int32 TurnBehaviour::turn
	int32_t ___turn_2;
	// TurnBehaviour TurnBehaviour::instance
	TurnBehaviour_t179487338 * ___instance_3;

public:
	inline static int32_t get_offset_of_turn_2() { return static_cast<int32_t>(offsetof(TurnBehaviour_t179487338_StaticFields, ___turn_2)); }
	inline int32_t get_turn_2() const { return ___turn_2; }
	inline int32_t* get_address_of_turn_2() { return &___turn_2; }
	inline void set_turn_2(int32_t value)
	{
		___turn_2 = value;
	}

	inline static int32_t get_offset_of_instance_3() { return static_cast<int32_t>(offsetof(TurnBehaviour_t179487338_StaticFields, ___instance_3)); }
	inline TurnBehaviour_t179487338 * get_instance_3() const { return ___instance_3; }
	inline TurnBehaviour_t179487338 ** get_address_of_instance_3() { return &___instance_3; }
	inline void set_instance_3(TurnBehaviour_t179487338 * value)
	{
		___instance_3 = value;
		Il2CppCodeGenWriteBarrier((&___instance_3), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // TURNBEHAVIOUR_T179487338_H
#ifndef TURNCOUNTER_T2433859135_H
#define TURNCOUNTER_T2433859135_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// TurnCounter
struct  TurnCounter_t2433859135  : public MonoBehaviour_t1158329972
{
public:
	// System.Int32 TurnCounter::bestturns
	int32_t ___bestturns_2;

public:
	inline static int32_t get_offset_of_bestturns_2() { return static_cast<int32_t>(offsetof(TurnCounter_t2433859135, ___bestturns_2)); }
	inline int32_t get_bestturns_2() const { return ___bestturns_2; }
	inline int32_t* get_address_of_bestturns_2() { return &___bestturns_2; }
	inline void set_bestturns_2(int32_t value)
	{
		___bestturns_2 = value;
	}
};

struct TurnCounter_t2433859135_StaticFields
{
public:
	// System.Int32 TurnCounter::turncount
	int32_t ___turncount_3;
	// TurnCounter TurnCounter::instance
	TurnCounter_t2433859135 * ___instance_4;

public:
	inline static int32_t get_offset_of_turncount_3() { return static_cast<int32_t>(offsetof(TurnCounter_t2433859135_StaticFields, ___turncount_3)); }
	inline int32_t get_turncount_3() const { return ___turncount_3; }
	inline int32_t* get_address_of_turncount_3() { return &___turncount_3; }
	inline void set_turncount_3(int32_t value)
	{
		___turncount_3 = value;
	}

	inline static int32_t get_offset_of_instance_4() { return static_cast<int32_t>(offsetof(TurnCounter_t2433859135_StaticFields, ___instance_4)); }
	inline TurnCounter_t2433859135 * get_instance_4() const { return ___instance_4; }
	inline TurnCounter_t2433859135 ** get_address_of_instance_4() { return &___instance_4; }
	inline void set_instance_4(TurnCounter_t2433859135 * value)
	{
		___instance_4 = value;
		Il2CppCodeGenWriteBarrier((&___instance_4), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // TURNCOUNTER_T2433859135_H
#ifndef RATINGBEHAVIOUR_T2117563504_H
#define RATINGBEHAVIOUR_T2117563504_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// RatingBehaviour
struct  RatingBehaviour_t2117563504  : public MonoBehaviour_t1158329972
{
public:

public:
};

struct RatingBehaviour_t2117563504_StaticFields
{
public:
	// System.Single RatingBehaviour::totturns
	float ___totturns_2;
	// System.Single RatingBehaviour::curturns
	float ___curturns_3;
	// System.Single RatingBehaviour::turnratio
	float ___turnratio_4;
	// System.Int32 RatingBehaviour::rating
	int32_t ___rating_5;
	// RatingBehaviour RatingBehaviour::instance
	RatingBehaviour_t2117563504 * ___instance_6;

public:
	inline static int32_t get_offset_of_totturns_2() { return static_cast<int32_t>(offsetof(RatingBehaviour_t2117563504_StaticFields, ___totturns_2)); }
	inline float get_totturns_2() const { return ___totturns_2; }
	inline float* get_address_of_totturns_2() { return &___totturns_2; }
	inline void set_totturns_2(float value)
	{
		___totturns_2 = value;
	}

	inline static int32_t get_offset_of_curturns_3() { return static_cast<int32_t>(offsetof(RatingBehaviour_t2117563504_StaticFields, ___curturns_3)); }
	inline float get_curturns_3() const { return ___curturns_3; }
	inline float* get_address_of_curturns_3() { return &___curturns_3; }
	inline void set_curturns_3(float value)
	{
		___curturns_3 = value;
	}

	inline static int32_t get_offset_of_turnratio_4() { return static_cast<int32_t>(offsetof(RatingBehaviour_t2117563504_StaticFields, ___turnratio_4)); }
	inline float get_turnratio_4() const { return ___turnratio_4; }
	inline float* get_address_of_turnratio_4() { return &___turnratio_4; }
	inline void set_turnratio_4(float value)
	{
		___turnratio_4 = value;
	}

	inline static int32_t get_offset_of_rating_5() { return static_cast<int32_t>(offsetof(RatingBehaviour_t2117563504_StaticFields, ___rating_5)); }
	inline int32_t get_rating_5() const { return ___rating_5; }
	inline int32_t* get_address_of_rating_5() { return &___rating_5; }
	inline void set_rating_5(int32_t value)
	{
		___rating_5 = value;
	}

	inline static int32_t get_offset_of_instance_6() { return static_cast<int32_t>(offsetof(RatingBehaviour_t2117563504_StaticFields, ___instance_6)); }
	inline RatingBehaviour_t2117563504 * get_instance_6() const { return ___instance_6; }
	inline RatingBehaviour_t2117563504 ** get_address_of_instance_6() { return &___instance_6; }
	inline void set_instance_6(RatingBehaviour_t2117563504 * value)
	{
		___instance_6 = value;
		Il2CppCodeGenWriteBarrier((&___instance_6), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // RATINGBEHAVIOUR_T2117563504_H





#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1800 = { sizeof (RatingBehaviour_t2117563504), -1, sizeof(RatingBehaviour_t2117563504_StaticFields), 0 };
extern const int32_t g_FieldOffsetTable1800[5] = 
{
	RatingBehaviour_t2117563504_StaticFields::get_offset_of_totturns_2(),
	RatingBehaviour_t2117563504_StaticFields::get_offset_of_curturns_3(),
	RatingBehaviour_t2117563504_StaticFields::get_offset_of_turnratio_4(),
	RatingBehaviour_t2117563504_StaticFields::get_offset_of_rating_5(),
	RatingBehaviour_t2117563504_StaticFields::get_offset_of_instance_6(),
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1801 = { sizeof (Rayito_t1865598542), -1, 0, 0 };
extern const int32_t g_FieldOffsetTable1801[1] = 
{
	Rayito_t1865598542::get_offset_of_cam_2(),
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1802 = { sizeof (SceneLoading_t2546480756), -1, sizeof(SceneLoading_t2546480756_StaticFields), 0 };
extern const int32_t g_FieldOffsetTable1802[4] = 
{
	SceneLoading_t2546480756::get_offset_of_num_2(),
	SceneLoading_t2546480756_StaticFields::get_offset_of_gamewon_3(),
	SceneLoading_t2546480756_StaticFields::get_offset_of_gamelost_4(),
	SceneLoading_t2546480756::get_offset_of_testnum_5(),
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1803 = { sizeof (Swiping_t1887146935), -1, sizeof(Swiping_t1887146935_StaticFields), 0 };
extern const int32_t g_FieldOffsetTable1803[10] = 
{
	Swiping_t1887146935_StaticFields::get_offset_of_mydirection_2(),
	Swiping_t1887146935_StaticFields::get_offset_of_firstPressPos_3(),
	Swiping_t1887146935_StaticFields::get_offset_of_secondPressPos_4(),
	Swiping_t1887146935_StaticFields::get_offset_of_currentSwipe_5(),
	Swiping_t1887146935_StaticFields::get_offset_of_canswipe_6(),
	Swiping_t1887146935_StaticFields::get_offset_of_The_Dragged_7(),
	Swiping_t1887146935_StaticFields::get_offset_of_Swipe_8(),
	Swiping_t1887146935_StaticFields::get_offset_of_swiping_9(),
	Swiping_t1887146935::get_offset_of_eventSent_10(),
	Swiping_t1887146935::get_offset_of_lastPosition_11(),
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1804 = { sizeof (SwipeDirection_t3009127793)+ sizeof (RuntimeObject), sizeof(int32_t), 0, 0 };
extern const int32_t g_FieldOffsetTable1804[5] = 
{
	SwipeDirection_t3009127793::get_offset_of_value___1() + static_cast<int32_t>(sizeof(RuntimeObject)),
	0,
	0,
	0,
	0,
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1805 = { sizeof (TurnBehaviour_t179487338), -1, sizeof(TurnBehaviour_t179487338_StaticFields), 0 };
extern const int32_t g_FieldOffsetTable1805[2] = 
{
	TurnBehaviour_t179487338_StaticFields::get_offset_of_turn_2(),
	TurnBehaviour_t179487338_StaticFields::get_offset_of_instance_3(),
};
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize1806 = { sizeof (TurnCounter_t2433859135), -1, sizeof(TurnCounter_t2433859135_StaticFields), 0 };
extern const int32_t g_FieldOffsetTable1806[3] = 
{
	TurnCounter_t2433859135::get_offset_of_bestturns_2(),
	TurnCounter_t2433859135_StaticFields::get_offset_of_turncount_3(),
	TurnCounter_t2433859135_StaticFields::get_offset_of_instance_4(),
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
