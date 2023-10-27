using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1_9_15 {
    internal class Program {
        class Item {
            public static int num = 0;
            public Item(string n, int c, int s) {
                name = n;
                cost = c;
                amount = 0;
                stock = s;
                index = ++num;
            }
            public string name;
            public int cost;
            public int amount;
            public int index;
            public int stock;
            public int GetTotal() {
                return cost * amount;
            }
        }
        struct Country {
            static public int num = 0;
            public Country(string n, double m) {
                name = n;
                mul = m;
                index = ++num;
            }
            public int index;
            public string name;
            public double mul;
        }
        static void Main(string[] args) {
            List<Country> countrys = new() { new Country("TWD", 1), new Country("USD", 0.031), new Country("CNY", 0.23), new Country("JPY", 4.59) };
            List<Item> items = new() { new Item("潛水相機防丟繩", 199, 1), new Item("潛水配重帶", 460, 2), new Item("潛水作業指北針", 1100, 1) };
            bool exit = false;
            int now_c = 0;
            while (!exit) {
                Console.WriteLine("(1)商品列表 (2)新增至購物車 (3)自購物車刪除 (4)查看購物車 (5)結帳 (6)轉換幣值 (7)退出網站");
                Console.Write("輸入數字選擇功能：");
                int option = Convert.ToInt32(Console.ReadLine());
                int choice, count;
                switch (option) {
                case 1:
                    Console.WriteLine("商品列表：\n商品名稱　單價");
                    foreach (Item item in items) {
                        Console.WriteLine($"{item.index}.{item.name} ({countrys[now_c].name}){item.cost * countrys[now_c].mul}");
                    }
                    break;
                case 2:
                    foreach (Item item in items) {
                        Console.Write($"({item.index}){item.name} ");
                    }
                    Console.Write("\n輸入數字選擇商品：");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice > 3 || choice < 1) {
                        Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                    }
                    Console.Write("輸入數量：");
                    count = Convert.ToInt32(Console.ReadLine());
                    items[choice - 1].amount += count;
                    break;
                case 3:
                case 4:
                    Console.WriteLine("購物車內容：\n商品　單價　數量　小計");
                    foreach (Item item in items) {
                        Console.WriteLine($"{item.index}.{item.name} ({countrys[now_c].name}){item.cost * countrys[now_c].mul} {item.amount} {item.GetTotal() * countrys[now_c].mul}");
                    }
                    if (option == 4) break;
                    Console.Write("輸入數字選擇商品：");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice > 3 || choice < 1) {
                        Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                    }
                    Console.Write("輸入數量：");
                    count = Convert.ToInt32(Console.ReadLine());
                    if (count > items[choice - 1].amount) {
                        Console.WriteLine("商品數量不可為負!"); break;
                    }
                    items[choice - 1].amount -= count;
                    break;
                case 5:
                    Console.WriteLine("訂單內容：\n商品　單價　數量　小計");
                    int total = 0;
                    foreach (Item item in items) {
                        if (item.amount == 0) continue;
                        Console.WriteLine($"{item.name} ({countrys[now_c].name}){item.cost * countrys[now_c].mul} {item.amount} {item.GetTotal() * countrys[now_c].mul}");
                        total += item.GetTotal();
                    }
                    Console.WriteLine($"總價 = {total * countrys[now_c].mul}");
                    Console.Write("*是否要結帳(Y/N)*: ");
                    string? checkout = Console.ReadLine();
                    if (checkout != "Y") {
                        if (checkout != "N") Console.WriteLine("輸入錯誤! 請重新輸入!");
                        break;
                    }

                    bool p = true;
                    foreach (Item item in items) {
                        if (item.amount == 0) continue;
                        if (item.amount > item.stock) {
                            Console.WriteLine($"{item.name} 庫存不足! 剩餘數量{item.stock}");
                            p = false;
                        }
                    }
                    if (!p) break;

                    Console.Write("*選擇付款方式(1.線上付款 2.貨到付款): ");
                    int checkType = Convert.ToInt32(Console.ReadLine());
                    if (checkType > 2 || checkType < 1) {
                        Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                    }

                    Console.Write("*折扣碼(若無折扣碼則輸入N): ");
                    string? code = Console.ReadLine();
                    if (code != "N" && code != "1111") {
                        Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                    }

                    Console.WriteLine("\n訂單狀態:\n商品　單價　數量　小計");
                    foreach (Item item in items) {
                        if (item.amount == 0) continue;
                        Console.WriteLine($"{item.name} ({countrys[now_c].name}){item.cost * countrys[now_c].mul} {item.amount} {item.GetTotal() * countrys[now_c].mul}");
                    }
                    Console.WriteLine($"總價 = {total * countrys[now_c].mul}");
                    if (code == "1111") Console.WriteLine($"總價(折扣後) = {total * 0.95}");
                    Console.WriteLine($"狀態: {(checkType == 1 ? "已付款" : "尚未付款")}");
                    exit = true;

                    break;
                case 6:
                    Console.Write("選擇貨幣 ");
                    foreach (Country c in countrys) {
                        Console.Write($"{c.index}.{c.name} ");
                    }
                    Console.Write(": ");
                    int tmp = Convert.ToInt32(Console.ReadLine());
                    if (tmp < 1 || tmp > 4) {
                        Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                    }
                    now_c = tmp - 1;
                    break;
                case 7:
                    exit = true; break;
                default:
                    Console.WriteLine("輸入錯誤! 請重新輸入!"); break;
                }
                Console.WriteLine("");
            }
        }
    }
}