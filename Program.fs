module Program
open System
open System.IO
open Day1
open Day2

[<EntryPoint>]
let main argv =
    printfn "Advent of Code!"
    File.ReadLines("inputs/day1.txt") |> day1
    File.ReadLines("inputs/day2.txt") |> day2
    0
