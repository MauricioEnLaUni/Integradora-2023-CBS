import 'package:flutter/material.dart';
import 'package:front_end/constants.dart';
import 'package:front_end/src/wigets/custom_text.dart';
import '../models/user_dto.dart';
import '../Pages/menu/menu_page.dart';

AppBar topNavigationBar(BuildContext context, GlobalKey<ScaffoldState> key) =>
    AppBar(
      leading: Row(
        children: [
          Container(
            padding: const EdgeInsets.only(left: 14),
            child: Image.asset(
              "assets/cbs.png",
              width: 28,
            ),
          ),
          Expanded(
            child: IconButton(
            icon: Icon(Icons.arrow_back),
            onPressed: () {
              Navigator.pop(context);
            },
            )
          )
        ],
      ),
      elevation: 0,
      title: Row(
        children: [
          Visibility(
              child: CustomText(
            text: "Dash",
            color: blueBar,
            size: 20,
            weight: FontWeight.bold,
          )),
          Expanded(
            child: Container(),
          ),
          IconButton(
            icon: Icon(
              Icons.settings,
              color: shadow.withOpacity(.7),
            ),
            onPressed: () {},
          ),
          Stack(children: [
            IconButton(
                icon: Icon(
                  Icons.add,
                  color: shadow.withOpacity(.7),
                ),
                onPressed: () {}),
            Positioned(
              top: 7,
              right: 7,
              child: Container(
                width: 12,
                height: 12,
                padding: const EdgeInsets.all(4),
              ),
            )
          ]),
          Container(
            width: 1,
            height: 22,
            color: Colors.amber,
          ),
          const SizedBox(
            width: 24,
          ),
          const CustomText(
            text: ("usr.Name"),
            color: Colors.blueGrey,
          ),
          const SizedBox(
            width: 16,
          ),
          Container(
              decoration: BoxDecoration(
                  color: Colors.white, borderRadius: BorderRadius.circular(30)),
              child: Container(
                  padding: const EdgeInsets.all(2),
                  margin: const EdgeInsets.all(2),
                  child: CircleAvatar(
                      backgroundColor: pk,
                      child: const Icon(
                        Icons.person_off_outlined,
                        color: Colors.grey,
                      ))))
        ],
      ),

      // inconTheme: IconThemeData(color: Colors.black),
      // backgroundColor: Colors.transparent,
    );
