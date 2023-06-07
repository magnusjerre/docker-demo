package com.example.bulletin.models;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class BulletinBoardMessage {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;
    private String posterId;
    private String message;

    public BulletinBoardMessage() {
    }

    public BulletinBoardMessage(String posterId, String message) {
        this.posterId = posterId;
        this.message = message;
    }

    public long getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getPosterId() {
        return posterId;
    }

    public void setPosterId(String posterId) {
        this.posterId = posterId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
