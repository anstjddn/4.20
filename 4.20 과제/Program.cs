namespace _4._20_과제
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            for (int i = 1; i < 5; i++) list.Add(i);                    //과제로 제출한 List의 경우 i를 0부터 시작하면 에러가 걸려서 i를 1부터 해서 반복했다.

            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

        
            LinkedList<int> Linked = new LinkedList<int>();

            for (int j = 1; j < 5; j++) Linked.AddFirst(j);     //과제로 제출한 LinkedList AddFirst에 실수를해서 반복을해도 바로 끝나서  AddFirst부분만따로 복붙해서 해봤더니 
                                                                // 제대로 돌아간다.

            foreach (int j in Linked)
            {
                Console.WriteLine(j);
            }
                                                                    //
        }
    }
}