package com.example.bulletin.controllers;

import com.example.bulletin.dto.BulletinBoardMessageGetDto;
import com.example.bulletin.dto.BulletinBoardMessagePostDto;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

@RestController
@RequestMapping("bulletinboardmessages")
public class BulletinBoardMessageController {

    private List<BulletinBoardMessageGetDto> messages = new ArrayList<>();

    @GetMapping
    public Collection<BulletinBoardMessageGetDto> GetBulletinBoardMessages() {
        return messages;
    }

    @PostMapping
    public BulletinBoardMessageGetDto PostBulletinBoardMessage(@RequestBody BulletinBoardMessagePostDto message) {
        var messageAsGetDtos = new BulletinBoardMessageGetDto(messages.size() + 1, message.getPosterId(), message.getMessage());
        this.messages.add(messageAsGetDtos);
        return messageAsGetDtos;
    }
}
