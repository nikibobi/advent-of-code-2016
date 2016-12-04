module Program
open System
open System.IO
open Day1

[<EntryPoint>]
let main argv =
    printfn "Advent of Code!"
    File.ReadLines("inputs/day1.txt") |> day1
    0
