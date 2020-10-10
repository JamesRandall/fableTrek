module Tests.Rules

open Expecto

module Weapons =  
  open Game.Types
  open Game.Rules.Weapons

  [<Tests>]
  let tests =
    testList "Weapons" [
      testCase "Undamaged phasers scale temperature by 1" <| fun _ ->
        let player = Player.Default
        let temperatureIncrease = calculatePhaserTemperatureIncrease player 
        Expect.equal
          temperatureIncrease
          (player.PhaserTemperatureCostPerGigawatt * (player.PhaserPower.Current |> float))
          "Temperature increase scales by 1"

      testCase "50% damaged phases scale temperature by < 1.2" <| fun _ ->
        let player = { Player.Default with Phasers = { Player.Default.Phasers with Current = Player.Default.Phasers.Current / 2. } }
        let temperatureIncrease = calculatePhaserTemperatureIncrease player 
        System.Console.WriteLine(sprintf "%f" temperatureIncrease)
        Expect.isLessThan
          temperatureIncrease
          (player.PhaserTemperatureCostPerGigawatt * (player.PhaserPower.Current |> float) * 1.2)
          "Temperature increase scales by < 1.2"

      testCase "100% damaged phases scale temperature to maximum" <| fun _ ->
        let player = { Player.Default with Phasers = { Player.Default.Phasers with Current = 0.<hitpoints> } }
        let temperatureIncrease = calculatePhaserTemperatureIncrease player 
        System.Console.WriteLine(sprintf "%f" temperatureIncrease)
        Expect.equal
          temperatureIncrease
          Player.Default.PhaserTemperature.Max
          "Temperature increased to max"
    ]