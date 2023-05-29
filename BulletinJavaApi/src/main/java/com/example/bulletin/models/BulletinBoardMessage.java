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
    private long posterId;
    private String message;

    public BulletinBoardMessage() {
    }

    public BulletinBoardMessage(long posterId, String message) {
        this.posterId = posterId;
        this.message = message;
    }

    public long getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public long getPosterId() {
        return posterId;
    }

    public void setPosterId(long posterId) {
        this.posterId = posterId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
