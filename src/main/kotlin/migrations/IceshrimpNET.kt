package me.blueb.migrations

class IceshrimpNET {
    fun start(host: String) {
        println("")
        println("What is the host of your database server?")
        println("e.g. localhost\n")

        val dbhost = readlnOrNull().toString()

        println("")
        println("What is the port of your database server?")
        println("e.g. 5432\n")

        val dbport = readlnOrNull().toString()

        println("")
        println("What is the name of your database?")
        println("e.g. 5432\n")

        val dbname = readlnOrNull().toString()

        println("")
        println("What is the user for your database?")
        println("e.g. 5432\n")

        val dbuser = readlnOrNull().toString()

        println("")
        println("What is the password for your database?")
        println("e.g. 5432\n")

        val dbpass = readlnOrNull().toString()
    }
}