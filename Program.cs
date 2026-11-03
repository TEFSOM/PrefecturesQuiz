
class Program
{
    // ハフマン木のノードを表すクラス
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        public string Name { get; set; } = "";  // 都道府県名 (葉ノードのみ)
        public long Population { get; set; } = 0;      // 人口（重み）
        public string HuffmanCode { get; set; } = "";   // ハフマン符号
        public HuffmanNode? Left { get; set; }        // 左の子
        public HuffmanNode? Right { get; set; }     // 右の子
        public bool IsLeaf => Left is null && Right is null;    //葉か判定
        public int CompareTo(HuffmanNode? other) => Population.CompareTo(other?.Population);    //人口で比較
        public HuffmanNode(string name = "", long population = 0)  //初期化
        {
            Name = name;
            Population = population;
        }
        public static HuffmanNode operator +(HuffmanNode left, HuffmanNode right)  //合体
        {
            var ret = new HuffmanNode();
            ret.Name = left.Name + right.Name;
            ret.Population = left.Population + right.Population;
            ret.Left = left;
            ret.Right = right;
            return ret;
        }
        public void AssignCodes(string code)    //ハフマン符号割り当て
        {
            HuffmanCode = code; //自分のコード覚えておく
            Left?.AssignCodes(code + "0");  //(居るなら)左右の子によろしく頼む
            Right?.AssignCodes(code + "1");
        }
        public void PrintCodes()    //ハフマン木を辿って文字出力
        {
            if (HuffmanCode == ""){ Console.WriteLine($" 都道府県\t| 人口 [人]　| ハフマン符号\n" + new string('-',45)); }    //根ならヘッダ
            if (IsLeaf){ Console.WriteLine($" {Name} \t|{Population,12:###,###,###,###}| {HuffmanCode,-10}"); };    //葉なら名前とか符号とか
            Left?.PrintCodes();  //(居るなら)左右の子によろしく頼む
            Right?.PrintCodes();
        }
    }

    static void Main()
    {
        //とりあえずリストに詰めて初期化
        List<HuffmanNode> prefData = 
        [
            new("東京都", 14236627),
            new("神奈川県", 9193922),
            new("大阪府", 8759312),
            new("愛知県", 7448043),
            new("埼玉県", 7287325),
            new("千葉県", 6257225),
            new("兵庫県", 5317792),
            new("福岡県", 5077785),
            new("北海道", 4980272),
            new("静岡県", 3465667),
            new("茨城県", 2789856),
            new("広島県", 2682786),
            new("京都府", 2500173),
            new("宮城県", 2225414),
            new("新潟県", 2067144),
            new("長野県", 1953866),
            new("岐阜県", 1889576),
            new("群馬県", 1865934),
            new("栃木県", 1863329),
            new("岡山県", 1807160),
            new("福島県", 1710242),
            new("三重県", 1693225),
            new("熊本県", 1677302),
            new("鹿児島県", 1511783),
            new("沖縄県", 1466548),
            new("滋賀県", 1392008),
            new("奈良県", 1268181),
            new("山口県", 1262770),
            new("愛媛県", 1259907),
            new("長崎県", 1232040),
            new("青森県", 1139461),
            new("岩手県", 1124652),
            new("石川県", 1086596),
            new("大分県", 1076353),
            new("宮崎県", 1017628),
            new("山形県", 992297),
            new("富山県", 982147),
            new("香川県", 906828),
            new("秋田県", 881470),
            new("和歌山県", 863757),
            new("佐賀県", 780675),
            new("山梨県", 779276),
            new("福井県", 728588),
            new("徳島県", 674790),
            new("高知県", 642836),
            new("島根県", 628887),
            new("鳥取県", 523073),
        ];

        //優先度付きキュー（最小ヒープ）に全ての都道府県を詰め直す
        var pq = new PriorityQueue<HuffmanNode, long>();
        foreach (var it in prefData){ pq.Enqueue(it, it.Population); }

        //最小ヒープの要素が１つになるまで、(最小の要素を)２つ取り出して合体して（ハフマン木の枝繋いで）詰め直す
        while (pq.Count > 1)
        {
            var left = pq.Dequeue();
            var right = pq.Dequeue();   //２つ取り出して
            HuffmanNode parent = left + right;  //合体して
            pq.Enqueue(parent, parent.Population);  //詰めなおす
        }

        var root =　pq.Dequeue();    //最後に残ったのがハフマン木の根のはず
        root.AssignCodes("");   //コード割り当て
        root.PrintCodes();  //文字出力
    }
}