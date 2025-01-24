package me.blueb.migrations

import java.sql.*
import kotlinx.serialization.json.*
import java.util.Objects

class IceshrimpNET {
    /*
        Get shrimpnet ini
        Connect to database
        Convert database
        Adjust aster config
    */

    fun start() {
        println("")
        println("Where is your Iceshrimp.NET configuration.ini?")
        println("e.g. /home/user/shrimpfig.ini\n")

        val shrimpfig = readlnOrNull().toString()

        println("")
        println("Where is your Aster production.yaml?")
        println("e.g. /home/user/aster/config/production.yaml\n")

        val asterconfig = readlnOrNull().toString()

        /*
         val url = "jdbc:postgresql://$dbhost:$dbport/$dbname"

        try {
            val connection = DriverManager.getConnection(url, dbuser, dbpass)
            val statement = connection.createStatement()
            val resultSet = statement.executeQuery("SELECT * FROM note")

            while (resultSet.next()) {

                println(resultSet.getString("column_name"))
            }

            resultSet.close()
            statement.close()
            connection.close()

        } catch (e: SQLException) {
            e.printStackTrace()
        }
        */

        /*
        convert order:
        - user
        - note
        - note edit
        */
    }

    fun convertNote(shrimpNote: ResultSet): Any {
        val asterNote = object {
            var id = ""
            var apId = ""

            var userId = ""
            var replyingToId = ""
            var repeatId = ""

            var cw = ""
            var content = ""
            var visibility = ""
            var to = ""

            var createdAt = ""
            var updatedAt = ""
            var attachments = ""
        }

        asterNote.id = shrimpNote.getString("id")
        asterNote.apId = shrimpNote.getString("uri")

        asterNote.userId = shrimpNote.getString("userId")
        asterNote.replyingToId = shrimpNote.getString("replyId")
        asterNote.repeatId = shrimpNote.getString("renoteId")

        asterNote.cw = shrimpNote.getString("cw")
        asterNote.content = shrimpNote.getString("text")

        val shrimpVisibility = shrimpNote.getString("visibility");

        if (shrimpVisibility == "public") asterNote.visibility = "public"
        if (shrimpVisibility == "home") asterNote.visibility = "unlisted"
        if (shrimpVisibility == "followers") asterNote.visibility = "followers"
        if (shrimpVisibility == "specified") asterNote.visibility = "direct"

        // array
        asterNote.to = shrimpNote.getString("mentions")

        asterNote.createdAt = shrimpNote.getString("createdAt")

        // array
        asterNote.attachments = shrimpNote.getString("fileIds")

        return asterNote;
    }

    fun editNote(shrimpNote: ResultSet) {
        // update updatedAt on note and update values
    }
}