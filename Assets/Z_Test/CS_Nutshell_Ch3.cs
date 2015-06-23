

//  _////////////////////////////////////////////////_    _____  Chap. 3  _____
//
// Chap. 3 Creating Types in C#
//
//  _////////////////////////////////////////////////_    _____  Chap. 3  _____

public class CSNutShellCh3
{
    public float mFloat, mFlt;

    public CSNutShellCh3 () {}
    public CSNutShellCh3 ( int pInt) {}

// 1. Classes
    // Fields : 멤버 변수.
    /*
     * - The - readonly - modifier : 생성 이후 변경 불가.
     * - Field initialization - 기본값으로 세팅. 생성 이전에 초기화됨.
     * - Declaring multiple fields together  int a, b, c ; 이러 식. 
     * */
    
    // Methods
    /* Overloading methods  */
    void Foo(int x) {}    //float Foo(int x) {} // 에러.
    void Foo(ref int x) {} // OK.. out과 ref를 동시사용 은 안됨 (혼동)
    
    // Instance Constructors
    /*  생성자는 초기화 코드 : initialization code 를 실행. 메서드 같지만 이름과 리턴타입이 함축. */
    /* - Overloading constructors 
     *   다른 생성자 호출 가능
     *   public Apple (int x, int y) : this ( x )  이런 식.  불린 놈이 먼저 실행됨. 
     *
     * - Implicit parameterless constructors : C# 이 자동 생성.
     *   struct 는 이런 무인수 생성자는 intrinsic (원래 있는)  사용자가 지정 불가. 기본값 세팅함.
     *
     * - Constructor and field initialization order
     * - Nonpublic constructors : pool 에서 꺼내 쓰거나 하는 등의 용도..  */
     
    // Object Initializers  아래 예처럼 { } 이용하여 멤버 초기값 세팅 가능.
    
    // The This Reference [9/21]
    
    // Properties : get set 코드로 로직이 있는 멤버. [9/21]
    /* - Read-only and calculated properties : write-only 가능하지만 드물다. 
     * - Automatic properties { get; set; }  C# 3.0
     * - get and set accessibility : 아래 AccessiBility 예. 
     * - CLR property implementation : 내부적으로 get_XXX 이름으로 컴파일 됨. 인라인으로. 
     */
    
    // Indexers : string의 인덱스. [9/22]
    /* - Implementing and indexer public string this [int wordNum] { get .... set ... }
     * - CLR indexer implementation     */
    
    // Constants : 변하지 않는 static field. 컴파일에 산정되어 값이 사용될 때마다 값 치환 C++ macro 비슷.
    //    public const string myMsg = "Hello ";  // 선언과 동시 초기화
    //    static readonly 보다 더 제한적. 차이점 : 컴파일 시 값 산정한다는 점. readonly 는 어플마다 다를 수 있다.
    //      다른 버전에서 값이 바뀔 수 있을 경우.
    
    // Static Constructors : type 당 한번만 실행. (인스턴스 당이 아님)
    /*   static ClassName() { SomeFunction(); }  이 클래스가 사용되기 전에 자동으로 한번 실행.
     *   역할 : 타잎 초기화, 타잎의 static member 에 접근.
     *   unsafe, extern 에만 접근 가능.
     *   예외를 발생하면 그 타잎은 사용 불능.
     *   
     * - Static constructors and field initialization order
     *   Static 생성자 이전에 불림. */
    
    // Static Classes [9/30
    /*  static member 만 갖고, 상속할 수 없는 클래스.  예) System.Console, System.Math */
    
    // Finalizers : Class only Methods . GC 실행 전에 수행.  ~ClassSample()
    
    // Partial Types and Methods  여러 파일에 코드 분산
    /*   구현할 인터페이스를 각각 구성 가능.
     * - Partial methods  ...  void, implicitly private ..   C# 3.0에서 도입.
     *      partial void GetSomeValue();        // definition  코드 제네레이터에 보통 있음. 
     *
     *      partial void GetSomeValue() { 구현 }  // implementation  매뉴얼 작성. */
    
// 2. Inheritance [p80]

    // Polymorphism [10/1]
    
    // Casting and Reference Conversions : 암시적 upcast to base class / 명시적 downcast to subclass.
    /* - Upcasting : 캐스팅 후에도 동일한 객체임. objA == objB
     * - Downcasting
     * - The as operator : 다운캐스트 실패 시 null 세팅.
     * - The is operator : if (a is Stock)  */
     
    // Virtual Function Members : 상속을 위한 클래스. Methods, properties, indexers, events 모두 가능.
    //  생성자에서 부르는 것은 잠재적 위험.
    
    // Abstract Classes and Abstract Members  [10/7]
    //  추상 클래스는 초기화되지 않음.
    
    // Hiding Inherited Members
    //  동일한 이름으로 베이스클래스 속성을 숨긴다.  Warning 발생.
    //  new 를 통해 워닝 억제..
    /* - new VS override
     *   new 로 재정의한 함수는 부모클래스로 캐스팅 하면 상위 함수가 불린다..  */
    
    // Sealing Functions and Classes [10/8 86p]
    //  더 이상의 override 를 막기 위해 봉인.
    
    // The base Keyword : this 비슷.
    //  파생 클래스에서 상위 멤버 접근, 상위 생성자 콜.
    
    // Constructors and Inheritance
    /* - Implicit calling of the parameterless base-class constructor
     *   인수 없는 상위 생성자는 암시적으로 자동 실행.
     * - Constructor and field initialization order  순서 .. */
    
    // Overloading and Resolution : 상속.. 캐스트하면 어느 함수를 부를 것인가.. dynamic ..
    
// 3. The object Type [p89]
// System.object 는 궁극의 베이스. 뭐든 object 로 캐스트 가능. 밸류 타잎도 박싱을 통해 가능.

    // Boxing and Unboxing [10/9]
    //  Value type 을 reference type 으로
    // - Copying semantics of boxing and unboxing
    
    // Static and Runtime Type Checking
    //  int x = "5" ;  // static
    //  int z = (int) y; // Runtime; unboxing.. GetType 을 통해 형 확인.
    
    // The GetType(runtime) Method and typeof(compile time) Operator
    //  theObj.GetType() == typeof(theObj)   Name, FullName
    
    // The ToString Method  (overide 가능.)
    
    // Object Member Listing
    
// 4. Structs [p93]
    
    // Struct Construction Semantics [10/10]
    //  인수 없는 생성자 안됨.
    
// 5. Access Modifiers [p94]
    // public (assembly:file 밖에서 인식 가능), internal (assembly 내에서 오픈, non-nested type 기본값) private (class, struct 기본값) protected, protected internal
    
    // Examples
    
    // Friend Assemblies
    //    System.Runtime.CompilerServices.InternalsVisibleTo  strong name ?
    
    // Accessibility Capping  타잎의 접근성을 멤버도 따라간다.
    //    class C { public void Foo() {} }  C 가 internal 이므로 Foo도 마찬가지.
    
    // Restrictions on Access Modifiers  동일하게 오버라이드 해야.. 예외. protected internal => overide 는 protected 여야 함..
    
// 6. Interfaces [p96] 클래스와 비슷하지만 구현보다는 스펙을 제공.  [10/14]
//    멤버는 모두 '암시적 추상' 반면 클래스는 양쪽 모두.  implicitly public ...  구현하면 모두 퍼블릭.
//    클래스는 여러 인터페이스 구현 가능. 반면 상속은 한 클래스에서만. 구조체는 상속 불가.   down cast 가능. 
//    member, property, event, indexer 만 포함.

    // Extending an Interface :: 다른 인터페이스에서 파생 가능.
    
    // Explicit Interface Implementation
    //  두 인터페이스 멤버이름이 충돌할 때 I2.Foo(), I3.Foo() 이런식으로 구현 가능.. 리턴형은 다름. 부를려면 캐스트 해야..
    //    명시적으로 멤버 이름을 구현하는 이유는 특별한 함수를 숨기거나 혼동하지 않기 위해서.  예)  ISerializable ...
    
    // Implementing Interface Members Virtually [10/15]
    //    기본적으로 sealed 임.. virtual 로 해야 오버라이드 가능. 상위 클래스 / 인터페이스의 멤버를 불러도 파생된 함수가 불림.
    
    // Reimplementing an Interface in a Subclass
    /* - Alternatives to interface reimplementation
     *   문제점 : 베이스 메스드를 부를 방법이 없다..  기대하지 않았던 일.  sealed .. 고려해 봐야.  */
     
    // Interfaces and Boxing    

// 7. Enums [p102 10/20] 이름 지어진 숫자 상수
//      기본은 정수이며 바이트도 사용 가능.  enum SomeValue : byte { n1, n2 = 3, n3 }; ... 이렇게.

    // Enum Conversions
    
    // Flags Enums
    
    // Enum Operators
    
    // Type-Safety Issues
    

// 8. Nested Types [p105 10/13]

// 9. Generics [p106 10/13]
    
    
    
    
    
}
    



public class TheMain
{
    private decimal acs;
    public decimal AccessiBility
    {
        get { return acs; }
        private set { acs = value; }
    }
    public void ObjectInit()
    {
        CSNutShellCh3 theObj = new CSNutShellCh3 {
            mFloat = 15f,
            mFlt = 1.5f
        };
        CSNutShellCh3 theObj1 = new CSNutShellCh3 (3) {
            mFloat = 11f
        };
    }

}