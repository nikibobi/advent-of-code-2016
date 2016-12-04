module Program
open System
open Day1

[<EntryPoint>]
let main argv =
    printfn "Advent of Code!"
    System.IO.File.ReadLines("inputs/day1.txt") |> day1
    0
