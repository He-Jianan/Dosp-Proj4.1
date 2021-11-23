open System
open Akka.FSharp
open Akka.Remote


let config =
    Configuration.parse
        @"akka {
            actor {
                provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                debug : {
                    receive : on
                    autoreceive : on
                    lifecycle : on
                    event-stream : on
                    unhandled : on
                }
            }
            remote.helios.tcp {
                hostname = localhost
                port = 9003
            }
        }"

let system = System.create "RemoteTestClient" config

let RemoteServer = system.ActorSelection("akka.tcp://Project4@localhost:9002/user/Actor-MsgHandler")

[<EntryPoint>]
let main argv =
    //register test
    let regMsg1 = "reg,hjn,,,,,"
    RemoteServer <! regMsg1
    Threading.Thread.Sleep(1000)
    let regMsg2 = "reg,嘉然今天吃什么,,,,,"
    RemoteServer <! regMsg2
    Threading.Thread.Sleep(1000)
    let regMsg3 = "reg,乃琳,,,,,"
    RemoteServer <! regMsg3
    Threading.Thread.Sleep(1000)
    let regMsg4 = "reg,贝拉,,,,,"
    RemoteServer <! regMsg4
    Threading.Thread.Sleep(1000)

    //send test
    let sendMsg1 = "send,hjn,然然可爱捏,,,嘉然超话,嘉然今天吃什么"
    RemoteServer <! sendMsg1
    Threading.Thread.Sleep(1000)
    let sendMsg2 = "send,嘉然今天吃什么,然然不是你的电子宠物,,,ASOUL超话#乃琳超话,乃琳@贝拉"
    RemoteServer <! sendMsg2
    Threading.Thread.Sleep(1000)

    //subscribe test
    let subMsg = "sub,hjn,,,嘉然今天吃什么,,"
    RemoteServer <! subMsg
    Threading.Thread.Sleep(1000)

    //retweet test

    //query test
    let queryMsg = "query,hjn,,,,,"
    RemoteServer <! queryMsg
    Threading.Thread.Sleep(1000)

    //query tag test
    let quertMsg = "quert,,,,,嘉然超话,"
    RemoteServer <! quertMsg
    Threading.Thread.Sleep(1000)

    //query mention test
    let quermMsg = "querm,,,,,,嘉然今天吃什么"
    RemoteServer <! quermMsg
    Threading.Thread.Sleep(1000)
    0 // return an integer exit code
