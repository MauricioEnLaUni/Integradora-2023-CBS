import 'dart:html';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:front_end/src/helpers/responsiveness.dart';
import 'package:flutter/src/material/icons.dart';
import 'package:front_end/constants.dart';
import 'package:front_end/src/wigets/custom_text.dart';


AppBar topNavigationBar(BuildContext context, GlobalKey<ScaffoldState> key) =>
  AppBar(
    leading:
    Row(
      children: [
        Container(
          padding: EdgeInsets.only (left: 14),
          child: Image.asset("assets/cbs.png", width: 28,),
        ) // Container
      ],
    ),
    elevation: 0,
    title: Row(
      children: [
        Visibility(child: CustomText(text: "Dash", color: blueBar, size: 20, weight: FontWeight.bold,)),
        Expanded(child: Container(),),
        IconButton(icon: Icon(Icons.settings, color: shadow.withOpacity(.7),),
        onPressed: () {},
        ),

        Stack(
          children: [
          IconButton(icon: Icon(Icons.add, color: shadow.withOpacity(.7),),
          onPressed: () {
              
          }
          ),
          Positioned(
            top: 7,
            right: 7,
            child: Container(
              width: 12,
              height: 12,
              padding: EdgeInsets.all(4),
            ),
            )
        ]
        ),

        Container(
          width: 1,
          height: 22,
          color: Colors.amber,
        ),

        SizedBox(
          width: 24,
        ),

        CustomText(text: "nombre", color: Colors.blueGrey,),

        SizedBox(
          width: 16,
        ),

        Container(
          decoration: BoxDecoration(color: Colors.white,
          borderRadius: BorderRadius.circular(30)
          ),
          child: Container(
            padding: EdgeInsets.all(2),
            margin: EdgeInsets.all(2),
            child: CircleAvatar(
              backgroundColor: pk,
              child: Icon(Icons.person_off_outlined, color: Colors.grey,)
              ) 
          )
        )
      ],
    ),


    // inconTheme: IconThemeData(color: Colors.black),
    // backgroundColor: Colors.transparent,
  );