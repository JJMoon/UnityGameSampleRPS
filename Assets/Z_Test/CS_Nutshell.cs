using System;
using System.Collections.Generic;

public class CS_Nutshell
{
    string strVar;



    readonly int itsRO = 3;

    public CS_Nutshell (int pItsRO)
    {
        itsRO = pItsRO;
    }

    public CS_Nutshell(int pItsRO, float pFloat) : this(pItsRO)
    {
        Func<int, int> someAdd = x => x + x;

    }

    //CS_Nutshell nutAnA = new CS_Nutshell {mA = 3, mB = 7, mC = 8};
    //CS_Nutshell nutAnB = new CS_Nutshell(77) {mA = 3, mB = 7, mC = 8};
    public int mA, mB, mC;
    public CS_Nutshell()
    {
     
    }



    public void ChangeString (ref string theStr, out string pDesti)  // nutObj.ChangeString (ref strIsReference);
    {
        theStr = "Here is CS_Nutshell";
        pDesti = theStr.Substring (3);
    }




    // Chap. 1 Introducing C# and .Net framework
    /* unified type system..  
     * interface : has only members. (useful when multiple inheritance required .)
     * property(object's state ??.. button's color), methods, events.
     * strongly typed language
     * Red Gate’s .NET Reflector application  :: also decompiler.
     **/





    // Chap. 2 C# Language Basic
    /* Main () :: default entry point execution 
     * identifier : By convention, parameters, local variables, and private fields should be in camel case (e.g., myVari able), and
     *              other identifiers should be in Pascal case (e.g., MyMethod).
     * class @class ?? 
     * punctuators : { } ;
     * jagged array  ::   int[][] matrix = new int[3][];    var jaggedMat = new int[][]
     * 
     * 
     *    p39 stack, heap ... continue ..
     *
     * Stack : 함수에 들어갈때마다 논리적으로 메모리(로컬변수, 인수) 증가.
     * Heap : Ref 타입 변수 메모리 블럭.
     *
     * Definite Assignment : 변수 사용 전 반드시 할당.(자동)
     *
     * Default Values
     *   decimal d = default(decimal); // 기본값 얻는 키워드. default
     *
     * Parameters
     *   ref, out : reference 로 넘어 가고 옴. 비슷하나 out 은 함수콜 이전 할당이 불필요
     *
     * The params modifier
     *   여러 인자를 받을 수 있도록, 마지막에 사용가능.   int Func(params int[] intArr)
     *
     * var - Implicitly Typed Local Variables 
     *   var a = 33; // = int a = 33; // statically typed. 
     *
     *
     *
     * Expressions and Operators [p47]
     *   12 // constant expression
     *   23 * 34 // operator and operands ..
     *   C# operator : unary, binary, ternary 로 분류. binary : infix (가운데 위치)
     *
     * - Primary Expressions
     *    Log(1) // . : member lookup, () : method call.
     *
     * - Void Expressions  : 값이 없으므로 연산에 이용 불가. 
     *
     * - Assignment Expressions ( = )
     *     *= , <<= : compound assignment operators 
     *
     * - Operator Precedence(우선 연산) and Associativity (Left : 8 / 4 / 2 => 1 같은 경우. Right : x = y = 3; )
     *
     *
     *
     * Statements
     *  - Scope (c# 은 C++과 달리 양 방향으로 영역 확장. )
     *
     * Expression Statements [p52]
     *   할당, 증가, 함수 콜, 객체 생성
     *   Case .. goto case "some":  이런게 가능. 
     *   for loops [p56]
     *
     *
     *
     * 
     **/

    public struct Point
    {  // The assignment of a value-type instance always copies the instance.
        public int X, Y;
        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    void Chap2 ()
    {
        Point p1 = new Point (8, 9);
        Point p2 = p1;
        p2.Y = int.MinValue; // p2.Y-- == int.MaxValue

        int[] a = new int[1000];
        Point[] arrPo = new Point[1000]; // struct : OK, class Point : null Objects .. 

        int[,] matrix = new int[3, 3];
        int itIs3 = matrix.GetLength (0); // Size ..
    }


}

