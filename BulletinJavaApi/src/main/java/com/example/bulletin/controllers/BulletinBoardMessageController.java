package com.example.bulletin.controllers;

import com.example.bulletin.dto.BulletinBoardMessageGetDto;
import com.example.bulletin.dto.BulletinBoardMessagePostDto;
import com.example.bulletin.models.BulletinBoardMessage;
import com.example.bulletin.repositories.BulletinBoardMessageRepository;
import org.springframework.web.bind.annotation.*;

import java.util.Collection;

@RestController
@RequestMapping("bulletinboardmessages")
public class BulletinBoardMessageController {

    private final BulletinBoardMessageRepository repository;

    public BulletinBoardMessageController(BulletinBoardMessageRepository repository) {
        this.repository = repository;
    }

    @GetMapping
    public Collection<BulletinBoardMessageGetDto> GetBulletinBoardMessages() {
        return repository.findAll().stream().map(it -> new BulletinBoardMessageGetDto(it.getId(), it.getPosterId(), it.getMessage())).toList();
    }

    @PostMapping
    public BulletinBoardMessageGetDto PostBulletinBoardMessage(@RequestBody BulletinBoardMessagePostDto message) {
        var savedMessage = repository.save(new BulletinBoardMessage(message.getPosterId(), message.getMessage()));
        return new BulletinBoardMessageGetDto(savedMessage.getId(), savedMessage.getPosterId(), savedMessage.getMessage());
    }
}
