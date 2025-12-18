import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat-widget',
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-widget.html',
  styleUrl: './chat-widget.css',
})
export class ChatWidgetComponent {
  isOpen = false;
  messages: { text: string; sender: 'user' | 'ai' }[] = [
    { text: 'Hi! I am Arya, your AI health assistant. How can I help you today?', sender: 'ai' }
  ];
  userInput = '';

  toggleChat() {
    this.isOpen = !this.isOpen;
  }

  sendMessage() {
    if (!this.userInput.trim()) return;

    this.messages.push({ text: this.userInput, sender: 'user' });
    const userMsg = this.userInput.toLowerCase();
    this.userInput = '';

    // Mock AI Response
    setTimeout(() => {
      let response = "I'm not sure about that. Try searching our library.";
      if (userMsg.includes('appointment') || userMsg.includes('book')) {
        response = "I can help you book an appointment. Would you like to find a doctor?";
      } else if (userMsg.includes('symptom') || userMsg.includes('pain') || userMsg.includes('sick')) {
        response = "I'm sorry you're not feeling well. Have you tried our Symptom Checker?";
      } else if (userMsg.includes('hello') || userMsg.includes('hi')) {
        response = "Hello! How can I assist you with your health today?";
      }
      this.messages.push({ text: response, sender: 'ai' });
    }, 1000);
  }
}
