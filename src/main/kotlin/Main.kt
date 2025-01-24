package me.blueb

import me.blueb.migrations.IceshrimpNET

fun main() {
    println("Aster Migration Utility\n")
    println("What software are you migrating from?")
    println("0: Iceshrimp.NET\n")

    val from = readlnOrNull().toString()
    if (from != "0") throw Error("Invalid input $from")

    println("")
    println("What is the host of your instance?")
    println("e.g. example.com\n")

    val host = readlnOrNull().toString()

    if (from === "0") {
        IceshrimpNET().start(host)
    }
}