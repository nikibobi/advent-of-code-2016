module Program
open System
open System.IO
open Day1
open Day2
open Day3
open Day4

[<EntryPoint>]
let main argv =
    printfn "Advent of Code!"
    File.ReadLines("inputs/day1.txt") |> day1
    File.ReadLines("inputs/day2.txt") |> day2
    File.ReadLines("inputs/day3.txt") |> day3
    File.ReadLines("inputs/day4.txt") |> day4
    0
